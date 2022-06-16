using HealthInstitution.Core;

namespace HealthInstitution.Services
{
    internal class CreateReviewService : IAssignReview
    {
        private readonly Review _review;

        public CreateReviewService(int service, int hygiene, int satisfaction, int suggestion, string comment)
        {
            _review = new HospitalReview(service, hygiene, satisfaction, suggestion, comment);
        }

        public CreateReviewService(int service, int suggestion, string comment)
        {
            _review = new Review(service, suggestion, comment);
        }

        public void AssignReview(Examination examination)
        {
            examination.Review = _review;
        }

        public void AssignReview()
        {
            HospitalReview review = (HospitalReview)_review;
            Institution.Instance().ReviewRepository.Reviews.Add(review);
        }
    }
}
