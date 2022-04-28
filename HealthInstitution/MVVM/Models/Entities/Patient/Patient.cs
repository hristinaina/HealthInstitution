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
        private MedicalRecord _record;
        private List<Examination> _examinations;
        private List<Operation> _operations;

        [JsonProperty("Blocked")]
        public bool Blocked { get => _blocked; set { _blocked = value; } }
        public MedicalRecord Record { get => _record; set { _record = value; } }


        public Patient() { 
        }

        public Patient(bool blocked)
        {
            _blocked = blocked;
            _examinations = new List<Examination>();
            _operations = new List<Operation>();
        }

        public List<Examination> GetExaminations()
        {
            if (_examinations is null) {
                _examinations = new List<Examination>();
            }
            return _examinations;
        }

        public List<Operation> GetOperations()
        {
            if (_operations is null)
            {
                _operations = new List<Operation>();
            }

            return _operations;
        }
    }
}
