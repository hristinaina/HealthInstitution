using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IDoctorDaysOffRepository : IRepository
    {
        public List<DoctorDaysOff> FindByDoctorID(int doctorId);
        public List<DoctorDaysOff> GetDoctorDaysOff();
    }
}