using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class DoctorDaysOff
    {
        private int _doctorId;
        private int _daysOffId;

        public DoctorDaysOff(int doctoId, int daysOffId)
        {
            _doctorId = doctoId;
            _daysOffId = daysOffId;
        }
    }
}
