using HealthInstitution.Core;
using HealthInstitution.Desktop.MVVM.Models.Services.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services.PatientServices
{
    public class PatientAppointmentsService
    {
        private List<Examination> _examinations;
        private List<Operation> _operations;
        private AppointmentService _appointmentService;

        public PatientAppointmentsService(Patient patient)
        {
            _examinations = patient.Examinations;
            _operations = patient.Operations;
            _appointmentService = new AppointmentService();
        }

        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> allAppointments = new List<Appointment>();
            allAppointments.AddRange(_examinations);
            allAppointments.AddRange(_operations);
            return allAppointments;

        }
        public List<Appointment> GetFutureAppointments()
        {
            List<Appointment> futureAppointments = new List<Appointment>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (!_appointmentService.IsDone(appointment.Date))
                {
                    futureAppointments.Add(appointment);
                }
            }
            futureAppointments = futureAppointments.OrderBy(x => x.Date).ToList();
            return futureAppointments;
        }

        public List<Appointment> GetPastAppointments()
        {
            List<Appointment> pastAppointments = new List<Appointment>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (_appointmentService.IsDone(appointment.Date))
                {
                    pastAppointments.Add(appointment);
                }
            }
            pastAppointments = pastAppointments.OrderBy(x => x.Date).ToList();
            return pastAppointments;
        }
        public List<Appointment> GetPastExaminations()
        {
            List<Appointment> pastExaminations = new List<Appointment>();
            foreach (Appointment appointment in GetAllAppointments())
            {
                if (_appointmentService.IsDone(appointment.Date))
                {
                    pastExaminations.Add(appointment);
                }
            }
            pastExaminations = pastExaminations.OrderBy(x => x.Date).ToList();
            return pastExaminations;
        }

    }
}
