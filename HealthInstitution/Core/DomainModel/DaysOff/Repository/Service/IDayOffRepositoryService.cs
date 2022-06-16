using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IDayOffRepositoryService
    {
        public DayOff FindByID(int id);
        public int FindNewID();
        public List<DayOff> DoctorDaysOffToDaysOff(List<DoctorDaysOff> doctorDaysOff);
        public List<DayOff> GetDaysOff();
    }
}
