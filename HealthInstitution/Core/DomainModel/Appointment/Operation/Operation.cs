using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core
{
    public class Operation : Appointment
    {
        private int _durationInMin;

        [JsonProperty("Duration")]
        public int Duration { get => _durationInMin; set { _durationInMin = value; } }

        public Operation(int id, Doctor doctor, Patient patient, DateTime date, int duration)
        {
            ID = id;
            Doctor = doctor;
            Patient = patient;
            Date = date;
            Emergency = false;
            Duration = duration;
        }
        public Operation()
        {

        }
    }
}
