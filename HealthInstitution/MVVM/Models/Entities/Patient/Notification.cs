using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{ 
    public class Notification
    {
        private int _id;
        [JsonProperty("Id")]
        public int Id
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

        private int _timesPerDay;

        public int TimesPerDay
        {
            get { return _timesPerDay; }
            set { _timesPerDay = value; }
        }

        private int _daysLongitude;

        public int DaysLongitude
        {
            get { return _daysLongitude; }
            set { _daysLongitude = value; }
        }



        public Notification(string text, DateTime date, int patientId, int timesPerDay, int daysLongitude)
        {
            _text = text;
            _dateTime = date;
            _patientId = patientId;
            _timesPerDay = timesPerDay;
            _daysLongitude = daysLongitude;
        }
    }
}
