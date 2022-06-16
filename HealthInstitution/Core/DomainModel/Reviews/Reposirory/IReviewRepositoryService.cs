using System.Collections.Generic;

namespace HealthInstitution.Core.Reposirory
{
    public interface IReviewRepositoryService
    {
        public List<HospitalReview> GetReviews();
    }
}