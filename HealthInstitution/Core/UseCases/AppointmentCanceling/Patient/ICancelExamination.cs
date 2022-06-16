using HealthInstitution.Core;

namespace HealthInstitution.Services
{
    public interface ICancelExamination
    {
        public bool CancelExamination(Examination examination);
    }
}
