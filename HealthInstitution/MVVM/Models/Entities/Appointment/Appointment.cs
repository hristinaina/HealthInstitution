using System;
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

        public int GetId() => _id;
        public void SetId(int id) => _id = id;
        public Doctor GetDoctor() => _doctor;
        public void SetDoctor(Doctor doctor) => _doctor = doctor;
        public Patient GetPatient() => _patient;
        public void SetPatient(Patient patient) => _patient = patient;
        public Room GetRoom() => _room;
        public void SetRoom(Room room) => _room = room;
        public DateTime GetDateTime() => _dateTime;
        public void SetDateTime(DateTime dateTime) => _dateTime = dateTime;
        public bool GetEmergency() => _isEmergency;
        public void SetEmergency(bool emergency) => _isEmergency = emergency;
        public bool GetDone() => _isDone;
        public void SetDone(bool done) => _isDone = done;
    }
}