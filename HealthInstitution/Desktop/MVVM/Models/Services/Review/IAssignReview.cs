using HealthInstitution.Core;

namespace HealthInstitution.Services
{
    public interface IAssignReview
    {
        public void AssignReview(Examination examination);
        public void AssignReview();
    }
}
