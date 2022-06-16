using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IDoctorDaysOffRepository
    {
        public List<DoctorDaysOff> FindByDoctorID(int doctorId);
    }
}