using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services
{
    interface ISuggestAlternativeAppointment
    {
        public List<Examination> MakeAlternativeSuggestions(Patient patient, Core.Doctor doctor);
        public List<Examination> MakeAlternativeSuggestions(Patient patient, DateTime startTime, DateTime endTime, DateTime deadlineDate);
        public void AssignDoctor(Patient patient, DateTime startTime, List<Examination> suggestions, DateTime startDateTime);

    }
}
