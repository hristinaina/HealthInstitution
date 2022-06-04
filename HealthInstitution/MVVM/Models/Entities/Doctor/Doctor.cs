using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Doctor : User, IComparable, IComparable<Doctor>
    {
        private Specialization _specialization;
        private List<Examination> _examinations;
        private List<Operation> _operations;
        private List<DayOff> _daysOff;
        private double _rating;
        private List<string> _notifications;

        [JsonProperty("Specialization")]
        public Specialization Specialization { get => _specialization; set { _specialization = value; } }
        [JsonProperty("Rating")]
        public double Rating { get => _rating; set { _rating = value; } }
        [JsonIgnore]
        public List<Examination> Examinations { get => _examinations; set { _examinations = value; } }
        [JsonIgnore]
        public List<Operation> Operations { get => _operations; set { _operations = value; } }
        [JsonIgnore]
        public List<DayOff> DaysOff { get => _daysOff; set { _daysOff = value; } }
        public List<string> Notifications { get => _notifications; set { _notifications = value; } }

        public Doctor()
        {
            _daysOff = new List<DayOff>();
            _examinations = new List<Examination>();
            _operations = new List<Operation>();
            _daysOff = new List<DayOff>();
            _notifications = new List<string>();
        }

        public Doctor(Specialization specialization = Specialization.NONE) : this()
        {
            _specialization = specialization;
        }

        public Doctor(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public Doctor(string firstName, string lastName, Specialization specialization) : this(firstName, lastName)
        {
            _specialization = specialization;
        }

        public bool IsAvailable(DateTime dateTime, int durationInMin = 15)
        {

            Appointment interruptingAppointment = FindInterruptingAppointment(dateTime, durationInMin);
            if (interruptingAppointment is null) return true;
            return false;
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
                int duration = 15;
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

        // schedule for certain day and 3 days after
        public List<Appointment> GetSchedule(DateTime date, string type)
        {
            List<Appointment> appointments = new();
            List<Appointment> scheduledAppointments = new();
            if (type == nameof(Examination))
            {
                foreach (Examination examination in _examinations) appointments.Add(examination);
            } else
            {
                foreach (Operation operation in _operations) appointments.Add(operation);
            }

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Date >= date && date.AddDays(3) >= appointment.Date)
                    scheduledAppointments.Add(appointment);
            }

            return scheduledAppointments;
        }

        public virtual bool Equals(Doctor? doctor) {
            return FirstName == doctor.FirstName && LastName == doctor.LastName;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Doctor other = obj as Doctor; // avoid double casting
            if (other == null)
            {
                throw new ArgumentException("A Doctor object is required for comparison.", "obj");
            }

            return CompareTo(other);
        }

        public int CompareTo(Doctor other)
        {
            if (other is null)
            {
                return 1;
            }
            return -string.Compare(this.FirstName + this.LastName, other.FirstName + other.LastName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
