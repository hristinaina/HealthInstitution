﻿using Newtonsoft.Json;
using System;
namespace HealthInstitution.Core
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
        [JsonIgnore]
        public Doctor Doctor { get => _doctor; set { _doctor = value; } }
        [JsonIgnore]
        public Patient Patient { get => _patient; set { _patient = value; } }
        [JsonIgnore]
        public Room Room { get => _room; set { _room = value; } }

        public Appointment() { }

        public Appointment(Doctor doctor, Patient patient, DateTime datetime)
        {
            _id = -1;
            _doctor = doctor;
            _patient = patient;
            _dateTime = datetime;
        }

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

        public bool IsEditable()
        {
            return (Date - DateTime.Now).TotalDays > 2;
        }
    }
}