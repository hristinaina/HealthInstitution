using System.Collections.Generic;

namespace HealthInstitution.Core.Services.DoctorServices
{
    internal interface IChekTroll
    {
        public int GetCreatingAttempts(List<ExaminationChange> changes);
        public int GetEditingAttempts(List<ExaminationChange> changes);
    }
}