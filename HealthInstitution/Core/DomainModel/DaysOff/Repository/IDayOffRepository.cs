using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IDayOffRepository : IRepository
    {
        public DayOff FindByID(int id);

        public int FindNewID();

        public List<DayOff> DoctorDaysOffToDaysOff(List<DoctorDaysOff> doctorDaysOff);
        public List<DayOff> GetDaysOff();
    }
}