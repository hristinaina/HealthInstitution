using System;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Appointment
    {
        private Doctor _doctor;
        private DateTime _start;
        private Room _room;

        public Appointment(Doctor doctor, DateTime start, Room room) {
            _doctor = doctor;
            _start = start;
            _room = room;
        }

        public Doctor GetDoctor()
        {
            return _doctor;
        }
        public DateTime GetStart()
        {
            return _start;
        }
        public Room GetRoom()
        {
            return _room;
        }
    }
}