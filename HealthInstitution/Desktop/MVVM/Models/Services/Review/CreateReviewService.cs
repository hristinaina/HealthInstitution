using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Desktop.MVVM.Models.Services
{
    class CreateReviewService
    {
        Review _review;

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
