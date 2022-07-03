using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.UseCases.AppointmentScheduling.Secretary
{
    public interface ISecretaryScheduleAppointmentService
    {
        public bool ScheduleAppointment(Appointment appointment, int duration, bool validation = true);
    }
}
