using System.Collections.Generic;

namespace HealthInstitution.Core.Reposirory
{
    public class ReviewRepositoryService : IReviewRepositoryService
    {
        private IReviewRepository _repository;

        public ReviewRepositoryService()
        {
            _repository = Institution.Instance().ReviewRepository;
        }

        public List<HospitalReview> GetReviews()
        {
            return _repository.GetReviews();
        }
    }
}