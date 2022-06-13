using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Services.DoctorServices;

namespace HealthInstitution.Core.Services
{
    class DoctorService : IUserAvailability
    {
        private Doctor _doctor;
        private List<Examination> _examinations;
        private List<Operation> _operations;

        public DoctorService(Doctor doctor)
        {
            _doctor = doctor;
            _examinations = doctor.Examinations;
            _operations = doctor.Operations;
        }

        // schedule for certain day and 3 days after
        public List<Appointment> GetSchedule(DateTime date, string type)
        {
            List<Appointment> appointments = new();
            List<Appointment> scheduledAppointments = new();
            
            if (type == nameof(Examination))
            {
                foreach (Examination examination in _examinations) appointments.Add(examination);
            }
            else
            {
                foreach (Operation operation in _operations) appointments.Add(operation);
            }

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Date >= date && date.AddDays(3) >= appointment.Date)
                    scheduledAppointments.Add(appointment);
            }

            return scheduledAppointments;
        }

        public Appointment FindInterruptingAppointment(DateTime dateTime, int durationInMin = 15)
        // returns null if appointment can be reserved
        // else returns appointment that interrupts (scheduled appoint.) - for the next free appointment calculation
        {
            List<Appointment> appointments = new();
            DateTime dateTimeEnds = dateTime.AddMinutes(durationInMin);
            foreach (Examination examination in _examinations) appointments.Add(examination);
            foreach (Operation operation in _operations) appointments.Add(operation);
            foreach (Appointment appointment in appointments)
            {
                DateTime appointmentBegins = appointment.Date;
                int duration = 15;
                if (appointment.GetType() == typeof(Operation))
                {
                    Operation operation = (Operation)appointment;
                    duration = operation.Duration;
                }
                DateTime appointmentEnds = appointmentBegins.AddMinutes(duration);
                if (DateTime.Compare(appointment.Date.Date, dateTime.Date) != 0) continue;
                if (DateTime.Compare(dateTime, appointmentBegins) >= 0 &&
                     DateTime.Compare(dateTime, appointmentEnds) < 0) return appointment;
                if (DateTime.Compare(dateTime.AddMinutes(durationInMin), appointmentBegins) > 0 &&
                    DateTime.Compare(dateTime.AddMinutes(durationInMin), appointmentEnds) <= 0) return appointment;
                if (appointmentBegins < dateTime && dateTimeEnds < appointmentEnds) return appointment;
            }

            return null;
        }

        public bool IsAvailable(DateTime dateTime, int durationInMin = 15)
        {
            Appointment interruptingAppointment = FindInterruptingAppointment(dateTime, durationInMin);
            if (interruptingAppointment is null) return true;
            return false;
        }
    }


}
