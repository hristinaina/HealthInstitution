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
        [JsonIgnore]
        public List<ExaminationChange> ExaminationChanges
        {
            get
            {
                if (_examinationChanges == null) _examinationChanges = new List<ExaminationChange>();
                return _examinationChanges;
            }
            set
            {
                _examinationChanges = value;
            }
        }
        public Patient()
        {
        }


        public void BlockPatient()
        {
            _blocked = true;
            _blockadeType = BlockadeType.SECRETARY;
            // TODO: ?delete all future appointments with this patient 
        }

        public void UnblockPatient()
        {
            _blocked = false;
            _blockadeType = BlockadeType.NONE;
            // TODO: ?delete all future appointments with this patient 
        }

        // constructor for when secretary is creating new patient accounts
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
    }
}
