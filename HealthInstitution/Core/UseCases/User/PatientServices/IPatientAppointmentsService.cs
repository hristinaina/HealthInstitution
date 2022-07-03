using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.UseCases.PatientServices
{
    public interface IPatientAppointmentsService
    {
        public List<Appointment> GetAllAppointments();

        public List<Appointment> GetFutureAppointments();

        public List<Appointment> GetPastAppointments();

        public List<Appointment> GetPastExaminations();
    }
}
