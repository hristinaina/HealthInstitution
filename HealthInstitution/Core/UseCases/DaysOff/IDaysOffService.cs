using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    public interface IDaysOffService
    {
        public void AcceptRequest(int id);
        public void RejectRequest(int id);
        public List<DayOff> FindByDoctorID(int id);
        public bool ValidateRequest(DayOff dayOff, Doctor doctor);
        public bool ApplyForDaysOff(DayOff dayOff, Doctor doctor);
    }
}
