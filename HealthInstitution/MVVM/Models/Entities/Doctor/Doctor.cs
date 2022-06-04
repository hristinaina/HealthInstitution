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
