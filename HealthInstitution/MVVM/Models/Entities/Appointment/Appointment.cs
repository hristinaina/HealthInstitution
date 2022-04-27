using System;
using Newtonsoft.Json;
namespace HealthInstitution.MVVM.Models.Entities
{
    abstract public class Appointment
    {
        private int _id;
        private Doctor _doctor;
        private Patient _patient;
        private Room _room;
        private DateTime _dateTime;
        private bool _isEmergency;
        private bool _isDone;

        [JsonProperty("ID")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("Date")]
        public DateTime Date { get => _dateTime; set { _dateTime = value; } }
        [JsonProperty("Emergency")]
        public bool Emergency { get => _isEmergency; set { _isEmergency = value; } }
        [JsonProperty("Done")]
        public bool Done { get => _isDone; set { _isDone = value; } }
        public Doctor Doctor { get => _doctor; set { _doctor = value; } }
        public Patient Patient { get => _patient; set { _patient = value; } }
        public Room Room { get => _room; set { _room = value; } }

        public Appointment(int id, DateTime dateTime, bool isEmergency, bool done)
        {
            _id = id;
            _doctor = null;
            _patient = null;
            _room = null;
            _dateTime = dateTime;
            _isEmergency = isEmergency;
            _isDone = done;
        }
    }
}