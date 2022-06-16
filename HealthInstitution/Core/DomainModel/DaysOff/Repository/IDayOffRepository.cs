using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IDayOffRepository
    {
        public DayOff FindByID(int id);

        public int FindNewID();

        public List<DayOff> DoctorDaysOffToDaysOff(List<DoctorDaysOff> doctorDaysOff);
    }
}