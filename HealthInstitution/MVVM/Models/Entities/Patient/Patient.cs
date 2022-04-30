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

        [JsonProperty("Blocked")]
        public bool Blocked { get => _blocked; set { _blocked = value; } }
        [JsonProperty("BlockadeType")]
        public BlockadeType BlockadeType { get => _blockadeType; set { _blockadeType = value; } }
        [JsonProperty("Deleted")]
        public bool Deleted { get => _deleted; set { _deleted = value; } }
        [JsonProperty("Record")]
        public MedicalRecord Record { get => _record; set { _record = value; } }
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

        // constructor used when secretary is creating new patient accounts
        public Patient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight, List<Allergen> allergens = null)
            : base(id, firstName, lastName, email, password, gender)
        {
            _blocked = false;
            _blockadeType = 0;
            _deleted = false;
            _record = new MedicalRecord(height, weight, allergens);
            // no need to fill _operations and _examinations lists because it is a new user so there would be none
        }

        public void UnblockPatient()
        {
            _blocked = false;
            _blockadeType = BlockadeType.NONE;
        }

        public List<Appointment> GetAllAppointments() {
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
            return futureAppointments;
        }

        public bool isTrolling()
        {

            if (GetEditingHistory() > 5)
            {
                return true;
            }
            if (GetCreatingHistory() > 8)
            {
                return true;
            }
            return false;
        }

        private int GetCreatingHistory()
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

        private int GetEditingHistory()
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

        public bool IsAvailable(DateTime startDateTime) {

            foreach (Appointment appointment in GetAllAppointments()) {
                if (DateTime.Compare(appointment.Date, startDateTime) < 0 && DateTime.Compare(appointment.Date, startDateTime) > 0)
                {
                    return false;
                }
                DateTime endDateTime = startDateTime.AddMinutes(15);
                if (DateTime.Compare(appointment.Date, endDateTime) < 0 && DateTime.Compare(appointment.Date, endDateTime) > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public List<string> GetHistoryOfIllness()
        {
            List<string> historyOfIllness = new List<string>();
            foreach (Examination examination in _examinations)
            {
                if (examination.Anamnesis.Length > 1) 
                    historyOfIllness.Add(examination.Anamnesis);
 
            }
            return historyOfIllness;
        }

        public List<string> GetHistoryOfIllness()
        {
            List<string> historyOfIllness = new List<string>();
            foreach (Examination examination in _examinations)
            {
                if (examination.Anamnesis.Length > 1) 
                    historyOfIllness.Add(examination.Anamnesis);
 
            }
            return historyOfIllness;
        }
    }
}
