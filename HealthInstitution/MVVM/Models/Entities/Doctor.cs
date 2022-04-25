using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Doctor : User
    {
        private Specialization _specialization;

        public Doctor(Specialization specialization) => _specialization = specialization;

        public Specialization GetSpecialization() => _specialization;
        public void SetSpecialization(Specialization specialization) => _specialization = specialization;
    }
}
