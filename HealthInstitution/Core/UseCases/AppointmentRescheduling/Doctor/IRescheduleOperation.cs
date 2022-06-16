using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;

namespace HealthInstitution.Services
{
    public interface IRescheduleOperation
    {
        public bool RescheduleAppointment(Appointment appointment, DateTime dateTime, bool validation = true);
        public bool RescheduleOperation(Operation operation, DateTime dateTime);
    }
}
