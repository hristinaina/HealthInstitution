using HealthInstitution.Core;
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
        private List<Examination> _operations;

        public PatientAppointmentsService(Patient patient)
        {
            _examinations = patient.Examinations;
            _operations = patient.Examinations;
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
                if (!isDone(appointment.Date))
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
                if (isDone(appointment.Date))
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
                if (isDone(appointment.Date))
                {
                    pastExaminations.Add(appointment);
                }
            }
            pastExaminations = pastExaminations.OrderBy(x => x.Date).ToList();
            return pastExaminations;
        }

        private bool isDone(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Now) < 0;
        }
    }
}
