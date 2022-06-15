using Newtonsoft.Json;
using System;
using HealthInstitution.Core;
using HealthInstitution.Core;

namespace HealthInstitution.Core
{
    public class DayOff
    {
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private bool _emergency;
        private string _reason;
        private State _state;
        private Doctor _doctor;

        [JsonProperty("ID")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("BeginDate")]
        public DateTime StartDate { get => _startDate; set { _startDate = value; } }
        [JsonProperty("EndDate")]
        public DateTime EndDate { get => _endDate; set { _endDate = value; } }
        [JsonProperty("Emergency")]
        public bool Emergency { get => _emergency; set { _emergency = value; } }
        [JsonProperty("Reason")]
        public string Reason { get => _reason; set { _reason = value; } }
        [JsonProperty("State")]
        public State State { get => _state; set { _state = value; } }
        [JsonIgnore]
        public Doctor Doctor { get => _doctor; set { _doctor = value; } }


        public DayOff(int id, DateTime beginDate, DateTime endDate, bool emergency,
                      string reason, int state)
        {
            _id = id;
            _startDate = beginDate;
            _endDate = endDate;
            _emergency = emergency;
            _reason = reason;
            _state = (State)state;
            _doctor = null;
        }

        public DayOff(int id, DateTime beginDate, DateTime endDate, bool emergency,
                      string reason, Doctor doctor)
        {
            _id = id;
            _startDate = beginDate;
            _endDate = endDate;
            _emergency = emergency;
            _reason = reason;
            _state = State.ON_HOLD;
            _doctor = doctor;
        }

        public DayOff()
        {

        }
    }
}