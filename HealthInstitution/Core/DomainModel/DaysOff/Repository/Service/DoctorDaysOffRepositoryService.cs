using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class DoctorDaysOffRepositoryService : IDoctorDaysOffRepositoryService
    {
        private readonly IDoctorDaysOffRepository _doctorDaysOffRepository;

        public DoctorDaysOffRepositoryService()
        {
            _doctorDaysOffRepository = Institution.Instance().DoctorDaysOffRepository;
        }

        public List<DoctorDaysOff> FindByDoctorID(int doctorId)
        {
            return _doctorDaysOffRepository.FindByDoctorID(doctorId);
        }

        public List<DoctorDaysOff> GetDoctorDaysOff()
        {
            return _doctorDaysOffRepository.GetDoctorDaysOff();
        }
    }
}
