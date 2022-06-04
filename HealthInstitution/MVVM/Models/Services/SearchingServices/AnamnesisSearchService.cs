using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services.SearchingServices
{
    class AnamnesisSearchService : SearchService
    {
        List<Appointment> _apointments;
        public AnamnesisSearchService(Patient patient)
        {
            _apointments = patient.GetAllAppointments();
        }

        public List<Appointment> SearchByAnamnesis(string keyWord)
        {
            List<Appointment> searchResults = new List<Appointment>();
            foreach (Appointment appointment in _apointments)
            {
                if (appointment is Examination examination)
                {
                    if (isMatching(examination.Anamnesis, keyWord))
                    {
                        searchResults.Add(appointment);
                    }
                }
            }
            return searchResults;
        }

    }
}
