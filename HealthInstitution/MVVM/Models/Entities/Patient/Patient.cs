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
        private MedicalRecord _record;
        private List<Examination> _examinations;
        private List<Operation> _operations;
        private List<ExaminationChange> _examinationChanges;

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


        // constructor for when blocking a patient account
        public void BlockPatient(bool blocked)
        {
            _blocked = blocked;
            // TODO: ?delete all future appointments with this patient 
        }

        public List<Appointment> GetFutureExaminations()
        {
            List<Appointment> futureExaminations = new List<Appointment>();
            foreach (Examination examination in _examinations)
            {
                if (DateTime.Compare(examination.Date, DateTime.Now) > 0)
                {
                    futureExaminations.Add(examination);
                }
            }
            return futureExaminations;
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
            foreach (ExaminationChange change in _examinationChanges)
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
            foreach (ExaminationChange change in _examinationChanges)
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
    }
}
