using System.Collections.Generic;
using HealthInstitution.Core.Reposirory;

namespace HealthInstitution.Core.Services.SurveyResults
{
    public class HospitalSurveyResultsService : IHospitalSurveyResultsService
    {
        private IReviewRepositoryService _reviews;

        public HospitalSurveyResultsService()
        {
            _reviews = new ReviewRepositoryService();
        }

        public Dictionary<string, List<double>> GetResults()
        {
            Dictionary<string, List<double>> results = new Dictionary<string, List<double>>()
            {
                ["Service"] = new List<double>() { 0, 0, 0, 0, 0, 0},
                ["Suggestion"] = new List<double>() { 0, 0, 0, 0, 0, 0},
                ["Hygiene"] = new List<double>() { 0, 0, 0, 0, 0, 0},
                ["Satisfaction"] = new List<double>() { 0, 0, 0, 0, 0, 0}
            };

            foreach (HospitalReview review in _reviews.GetReviews())
            {
                results["Service"][review.Service]++;
                results["Suggestion"][review.Suggestion]++;
                results["Hygiene"][review.Hygiene]++;
                results["Satisfaction"][review.Satisfacion]++;
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

        public List<string> GetComments()
        {
            List<string> comments = new List<string>();

            foreach (HospitalReview review in _reviews.GetReviews())
            {
                if (review.Comment is null || review.Comment.Equals(""))
                {
                    continue;
                }
                comments.Add(review.Comment);
            }

            return comments;
        }
    }
}