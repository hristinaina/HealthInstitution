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

        public Operation(int id, DateTime date, bool isEmergency, bool done, int duration) 
                         : base(id, date, isEmergency, done)
        {
            _durationInMin = duration;
        }

        public int GetDurationInMin() => _durationInMin;
        public void SetDurationInMin(int duration) => _durationInMin = duration;

        public void Update(Operation operation)
        {
            SetDateTime(operation.GetDateTime());
            SetEmergency(operation.GetEmergency());
            SetDone(operation.GetDone());
            SetRoom(operation.GetRoom());
        }

        public void Delete(int id)
        {

        }
    }
}
