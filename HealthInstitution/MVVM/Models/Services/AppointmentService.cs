using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    public static class AppointmentService
    {
        public static List<Appointment> SearchByAnamnesis(int patientId, string keyWord)
        {
            List<Appointment> searchResults = new List<Appointment>();
            Patient patient = Institution.Instance().PatientRepository.FindByID(patientId);
            foreach (Appointment appointment in patient.GetPastAppointments())
            {
                if (appointment is Examination examination)
                {
                    if (examination.Anamnesis.ToLower().Contains(keyWord.ToLower()))
                    {
                        searchResults.Add(appointment);
                    }
                }
            }
            return searchResults;
        }
    }
}
