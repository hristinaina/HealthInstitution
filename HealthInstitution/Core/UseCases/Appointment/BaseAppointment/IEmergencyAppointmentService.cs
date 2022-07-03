using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.UseCases
{
    public interface IEmergencyAppointmentService
    {
        public void ChangeDuration(int duration);

        public bool CreateEmergencyAppointment(Specialization specialization, Patient patient);

        public Appointment FindAppointment(Patient patient, Doctor doctor, DateTime newDate);

        public void FindNewAppointmentTime(Appointment appointment, int duration, Dictionary<Appointment, DateTime> appointments);

        public void NotifyDoctor();

        public void SendNotifications(Appointment rescheduledAppointment, Appointment newAppointment);
    }
}
