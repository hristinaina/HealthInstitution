using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Operation : Appointment
    {
        private int _durationInMin;

        [JsonProperty("Duration")]
        public int Duration { get => _durationInMin; set { _durationInMin = value; } }


        public Operation(int id, DateTime date, bool isEmergency, bool done, int duration) 
                         : base(id, date, isEmergency, done)
        {
            _durationInMin = duration;
        }

        public int GetDurationInMin() => _durationInMin;
        public void SetDurationInMin(int duration) => _durationInMin = duration;
    }
}
