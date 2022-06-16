using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core
{
    public class Notification
    {
        private int _id;
        [JsonProperty("Id")]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _text;
        [JsonProperty("Text")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private DateTime _dateTime;
        [JsonProperty("DateTime")]
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

        private int _patientId;
        [JsonProperty("PatientId")]
        public int PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        public Notification() { }

        public Notification(int id, string text, DateTime date, int patientId)

        {
            _id = id;
            _text = text;
            _dateTime = date;
            _patientId = patientId;
        }
    }
}
