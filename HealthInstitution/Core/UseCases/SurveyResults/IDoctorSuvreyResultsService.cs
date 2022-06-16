using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface IDoctorSuvreyResultsService
    {
        public Dictionary<string, List<double>> GetResults(Doctor doctor);

        public List<string> GetComments(Doctor doctor);
    }
}