using HealthInstitution.Core;
using HealthInstitution.Core.Services.PatientServices;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public class AnamnesisSearchService : SearchService, IAnamnesisSearch
    {
        private readonly List<Appointment> _apointments;
        public AnamnesisSearchService(Patient patient)
        {
            PatientAppointmentsService service = new PatientAppointmentsService(patient);
            _apointments = service.GetAllAppointments();
        }

        public List<Appointment> SearchByAnamnesis(string keyWord)
        {
            List<Appointment> searchResults = new List<Appointment>();
            foreach (Appointment appointment in _apointments)
            {
                if (appointment is Examination examination)
                {
                    if (IsMatching(examination.Anamnesis, keyWord))
                    {
                        searchResults.Add(appointment);
                    }
                }
            }
            return searchResults;
        }

    }
}
