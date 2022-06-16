using HealthInstitution.Core;
using HealthInstitution.Core.Services.PatientServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services.DoctorServices
{
    class PatientService : IUserAvailability
    {
        private List<Appointment> _appointments;

        public PatientService(Patient patient)
        {
            PatientAppointmentsService service = new PatientAppointmentsService(patient);
            _appointments = service.GetAllAppointments();
        }

        public PatientService()
        {
        }

        public bool IsAvailable(DateTime startDateTime, int durationInMin = 15)
        {
            return FindInterruptingAppointment(startDateTime, durationInMin) is null;
        }

        public Appointment FindInterruptingAppointment(DateTime startDateTime, int durationInMin = 15)
        {
            DateTime endDateTime = startDateTime.AddMinutes(durationInMin);
            foreach (Appointment appointment in _appointments)
            {
                DateTime appointmentBegin = appointment.Date;
                int duration = 15; ;
                if (appointment is Operation operation)
                {
                    duration = operation.Duration;
                }
                DateTime appointmentEnd = appointmentBegin.AddMinutes(duration);
                if (DateTime.Compare(startDateTime, appointmentBegin) >= 0 &&
                    DateTime.Compare(startDateTime, appointmentEnd) < 0)
                {
                    return appointment;
                }
                if (DateTime.Compare(endDateTime, appointmentBegin) > 0 &&
                    DateTime.Compare(endDateTime, appointmentEnd) <= 0)
                {
                    return appointment;
                }
            }
            return null;
        }
    }
}
