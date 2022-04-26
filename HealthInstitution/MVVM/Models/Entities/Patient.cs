using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Patient : User
    {
        public bool Blocked { get; set; }
        public MedicalRecord Record { get; set; }

        public Patient() {
            Record = new MedicalRecord();
        }
    }
}
