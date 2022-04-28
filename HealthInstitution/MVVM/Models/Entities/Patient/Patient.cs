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
        private MedicalRecord _record;
        private List<Examination> _examinations;
        private List<Operation> _operations;

        [JsonProperty("Blocked")]
        public bool Blocked { get => _blocked; set { _blocked = value; } }
        [JsonProperty("BlockadeType")]
        public BlockadeType BlockadeType { get => _blockadeType; set { _blockadeType = value; } }
        [JsonProperty("Record")]
        public MedicalRecord Record { get => _record; set { _record = value; } }
        [JsonIgnore]
        public List<Examination> Examinations
        {
            get
            {
                if (_examinations == null) _examinations = new List<Examination>();
                return _examinations;
            }
            set
            {
                _examinations = value;
            }
        }
        [JsonIgnore]
        public List<Operation> Operations
        {
            get
            {
                if (_operations == null) _operations = new List<Operation>();
                return _operations;
            }
            set
            {
                _operations = value;
            }
        }

        public Patient()
        {
        }

        // constructor for when blocking a patient account
        public void BlockPatient(bool blocked)
        {
            _blocked = blocked;
            // TODO: ?delete all future appointments with this patient 
        }

        // constructor for when secretary is creating new patient accounts
        public Patient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight, List<Allergen> allergens = null)
            : base(id, firstName, lastName, email, password, gender)
        {
            _blocked = false;
            _blockadeType = 0;
            _record = new MedicalRecord(height, weight, allergens);
            // no need to fill _operations and _examinations lists because it is a new user so there would be none
        }
    }
}
