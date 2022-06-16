using HealthInstitution.Core.Repository;
using System.Collections.Generic;

namespace HealthInstitution.Core.Services.SurveyResults
{
    public class DoctorSurveyResultsService
    {
        private IExaminationRepositoryService _examinations;

        public DoctorSurveyResultsService()
        {
            _examinations = new ExaminationRepositoryService();
        }

        public Dictionary<string, List<double>> GetResults(Doctor doctor)
        {
            Dictionary<string, List<double>> results = new Dictionary<string, List<double>>()
            {
                ["Service"] = new List<double>() { 0, 0, 0, 0, 0, 0 },
                ["Suggestion"] = new List<double>() { 0, 0, 0, 0, 0, 0 },
            };

            foreach (Examination examination in _examinations.GetExaminations())
            {
                if (examination.Doctor.Equals(doctor) && examination.Review is not null)
                {
                    results["Service"][examination.Review.Service]++;
                    results["Suggestion"][examination.Review.Suggestion]++;
                }
            }

            return GetAverage(results);
        }

        private Dictionary<string, List<double>> GetAverage(Dictionary<string, List<double>> survey)
        {
            foreach (string category in survey.Keys)
            {
                int sumOfGrades = 0;
                int numOfGrades = 0;

                for (int i = 1; i <= 5; i++)
                {
                    sumOfGrades += i * (int)survey[category][i];
                    numOfGrades += (int)survey[category][i];
                }

                survey[category][0] = (double)sumOfGrades / (double)numOfGrades;
            }
            return survey;
        }

        public List<string> GetComments(Doctor doctor)
        {
            List<string> comments = new List<string>();


            foreach (Examination examination in _examinations.GetExaminations())
            {
                if (examination.Doctor.Equals(doctor) && examination.Review is not null)
                {
                    comments.Add(examination.Review.Comment);
                }
            }

            return comments;
        }
    }
}