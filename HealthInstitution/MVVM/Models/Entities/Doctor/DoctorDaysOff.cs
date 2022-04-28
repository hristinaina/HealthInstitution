using Newtonsoft.Json;
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

        [JsonProperty("DoctorID")]
        public int DoctorID { get => _doctorId; set { _doctorId = value; } }
        [JsonProperty("DaysOffId")]
        public int DaysOffId { get => _daysOffId; set { _daysOffId = value; } }

        public DoctorDaysOff()
        {

        }

        public DoctorDaysOff(int doctoId, int daysOffId)
        {
            _doctorId = doctoId;
            _daysOffId = daysOffId;
        }
    }
}
