using System;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models
{
    public class DayOff
    {
        private int _id;
        private DateTime _beginDate;
        private DateTime _endDate;
        private bool _emergency;
        private string _reason;
        private State _state;
        private Doctor _doctor;

        public DayOff(int id, DateTime beginDate, DateTime endDate, bool emergency,
                      string reason, int state)
        {
            _id = id;
            _beginDate = beginDate;
            _endDate = endDate;
            _emergency = emergency;
            _reason = reason;
            _state = (State)state;
            _doctor = null;
        }

        public int GetId() => _id;
    }
}