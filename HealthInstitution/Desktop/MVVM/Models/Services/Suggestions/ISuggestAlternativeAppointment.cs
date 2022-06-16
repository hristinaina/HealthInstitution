using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services
{
    interface ISuggestAlternativeAppointment
    {
        protected List<Examination> MakeAlternativeSuggestions(Patient patient, Doctor doctor);
        protected List<Examination> MakeAlternativeSuggestions(Patient patient, DateTime startTime, DateTime endTime, DateTime deadlineDate);

    }
}
