using System.Collections.Generic;

namespace HealthInstitution.Core.Reposirory
{
    public interface IReviewRepository : IRepository
    {
        public List<HospitalReview> GetReviews();
    }
}