using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.UseCases.AppointmentCanceling.Secretary
{
    public interface ISecretaryDeleteAppointmentService
    {
        public void DeleteFutureAppointments(Patient patient);

        public void DeleteAppointment(Appointment appointment);
    }
}
