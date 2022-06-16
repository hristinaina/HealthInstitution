using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface IHospitalSurveyResultsService
    {
        public Dictionary<string, List<double>> GetResults();

        public List<string> GetComments();
    }
}