using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Patient : User
    {
        private bool _blocked;
        private BlockadeType _blockadeType;
        private bool _deleted;
        private MedicalRecord _record;
        private List<Examination> _examinations;
        private List<Operation> _operations;
        private List<ExaminationChange> _examinationChanges;
        private List<Notification> _notifications;
        private int _notificationsPreference;

        [JsonIgnore]
        public List<Notification> Notifications
        {
            get
            {
                if (_notifications == null)
                {
                    _notifications = new List<Notification>();
                }

                return _notifications;
            }
            set => _notifications = value;
        }
        [JsonProperty("Blocked")]
        public bool Blocked { get => _blocked; set { _blocked = value; } }
        [JsonProperty("BlockadeType")]
        public BlockadeType BlockadeType { get => _blockadeType; set { _blockadeType = value; } }
        [JsonProperty("Deleted")]
        public bool Deleted { get => _deleted; set { _deleted = value; } }
        [JsonProperty("Record")]
        public MedicalRecord Record { get => _record; set { _record = value; } }
        [JsonProperty("NotificationsPreference")]
        public int NotificationsPreference { get => _notificationsPreference; set { _notificationsPreference = value; } }
        [JsonIgnore]
        public List<Examination> Examinations
        {
            get
            {
                if (_examinations == null)
                {
                    _examinations = new List<Examination>();
                }

                return _examinations;
            }
            set => _examinations = value;
        }
        [JsonIgnore]
        public List<Operation> Operations
        {
            get
            {
                if (_operations == null)
                {
                    _operations = new List<Operation>();
                }

                return _operations;
            }
            set => _operations = value;
        }
        [JsonIgnore]
        public List<ExaminationChange> ExaminationChanges
        {
            get
            {
                if (_examinationChanges == null)
                {
                    _examinationChanges = new List<ExaminationChange>();
                }

                return _examinationChanges;
            }
            set => _examinationChanges = value;
        }
        public Patient()
        {
        }

        public Patient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
            : base(id, firstName, lastName, email, password, gender)
        {
            _blocked = false;
            _blockadeType = 0;
            _deleted = false;
            _record = new MedicalRecord(height, weight, new List<Allergen>(), new List<string>());
        }

        public void UnblockPatient()
        {
            _blocked = false;
            _blockadeType = BlockadeType.NONE;
        }

        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> allAppointments = new List<Appointment>();
            allAppointments.AddRange(_examinations);
            allAppointments.AddRange(_operations);
            return allAppointments;

        }
        public List<Appointment> GetFutureAppointments()
        {
            List<Appointment> futureAppointments = new List<Appointment>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (DateTime.Compare(appointment.Date, DateTime.Now) > 0)
                {
                    futureAppointments.Add(appointment);
                }
            }
            futureAppointments = futureAppointments.OrderBy(x => x.Date).ToList();
            return futureAppointments;
        }

        public List<Appointment> GetPastAppointments()
        {
            List<Appointment> pastAppointments = new List<Appointment>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (DateTime.Compare(appointment.Date, DateTime.Now) < 0)
                {
                    pastAppointments.Add(appointment);
                }
            }
            pastAppointments = pastAppointments.OrderBy(x => x.Date).ToList();
            return pastAppointments;
        }
        public List<Appointment> GetPastExaminations()
        {
            List<Appointment> pastExaminations = new List<Appointment>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (DateTime.Compare(appointment.Date, DateTime.Now) < 0)
                {
                    pastExaminations.Add(appointment);
                }
            }
            pastExaminations = pastExaminations.OrderBy(x => x.Date).ToList();
            return pastExaminations;
        }

        public bool IsTrolling()
        {
            if (GetEditingAttempts() > 5)
            {
                _blocked = true;
                _blockadeType = BlockadeType.SYSTEM;
                return true;
            }
            if (GetCreatingAttempts() > 8)
            {
                _blocked = true;
                _blockadeType = BlockadeType.SYSTEM;
                return true;
            }
            return false;
        }

        private int GetCreatingAttempts()
        {
            int totalCreations = 0;
            foreach (ExaminationChange change in ExaminationChanges)
            {
                if (change.ChangeStatus == AppointmentStatus.CREATED)
                {
                    totalCreations += 1;
                }
            }

            return totalCreations;
        }

        private int GetEditingAttempts()
        {
            int totalChanges = 0;
            foreach (ExaminationChange change in ExaminationChanges)
            {
                if (change.ChangeStatus == AppointmentStatus.EDITED)
                {
                    totalChanges += 1;
                }
                if (change.ChangeStatus == AppointmentStatus.DELETED)
                {
                    totalChanges += 1;
                }
            }

            return totalChanges;
        }

        public bool IsAvailable(DateTime startDateTime, int durationInMin = 15)
        {
            foreach (Examination examination in Examinations)
            {
                if (DateTime.Compare(examination.Date, startDateTime) <= 0 && DateTime.Compare(examination.Date.AddMinutes(durationInMin), startDateTime) >= 0)
                {
                    return false;
                }
                DateTime endDateTime = startDateTime.AddMinutes(15);
                if (DateTime.Compare(examination.Date, endDateTime) <= 0 && DateTime.Compare(examination.Date.AddMinutes(durationInMin), endDateTime) >= 0)
                {
                    return false;
                }
            }
            foreach (Operation operation in Operations)
            {
                if (DateTime.Compare(operation.Date, startDateTime) <= 0 && DateTime.Compare(operation.Date.AddMinutes(operation.Duration), startDateTime) >= 0)
                {
                    return false;
                }
                DateTime endDateTime = startDateTime.AddMinutes(15);
                if (DateTime.Compare(operation.Date, endDateTime) <= 0 && DateTime.Compare(operation.Date.AddMinutes(operation.Duration), endDateTime) >= 0)
                {
                    return false;
                }
            }

            return true;
        }

        public Appointment FindInterruptingAppointment(DateTime dateTime, int durationInMin = 15)
        // returns null if appointment can be reserved
        // else returns appointment that interrupts (scheduled appoint.) - for the next free appointment calculation
        {
            List<Appointment> appointments = new();
            foreach (Examination examination in _examinations) appointments.Add(examination);
            foreach (Operation operation in _operations) appointments.Add(operation);
            foreach (Appointment appointment in appointments)
            {
                DateTime appointmentBegin = appointment.Date;
                int duration = 15; ;
                if (appointment.GetType() == typeof(Operation))
                {
                    Operation operation = (Operation)appointment;
                    duration = operation.Duration;
                }
                DateTime appointmentEnd = appointmentBegin.AddMinutes(duration);
                if (DateTime.Compare(appointment.Date.Date, dateTime.Date) != 0) continue;
                if (DateTime.Compare(dateTime, appointmentBegin) >= 0 &&
                    DateTime.Compare(dateTime, appointmentEnd) < 0) return appointment;  
                if (DateTime.Compare(dateTime.AddMinutes(durationInMin), appointmentBegin) > 0 &&
                    DateTime.Compare(dateTime.AddMinutes(durationInMin), appointmentEnd) <= 0)
                    return appointment;
            }

            return null;
        }

        public List<string> GetHistoryOfIllness()
        {
            List<string> historyOfIllness = new List<string>();
            if (_examinations == null) return new List<string>();
            foreach (string illness in Record.HistoryOfIllnesses)
            {
                historyOfIllness.Add(illness);
            }
            return historyOfIllness;
        }

        public bool IsAllergic(List<Allergen> allergens)
        {
            foreach (Allergen i in _record.Allergens)
            {
                foreach(Allergen allergen in allergens)
                {
                    if (i.ID == allergen.ID) return true;
                }
            }

            return false;
        }

        public void Update(int id, string name, string lastName, string email, string password, Gender gender, double height, double weight)
        {
            ID = id;
            FirstName = name;
            LastName = lastName;
            Email = email;
            Password = password;
            Gender = gender;
            Record.Height = height;
            Record.Weight = weight;
        }
    }
}