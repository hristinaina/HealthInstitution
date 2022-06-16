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

        public DoctorDaysOffRepositoryService(IDoctorDaysOffRepository doctorDaysOffRepository)
        {
            _doctorDaysOffRepository = doctorDaysOffRepository;
        }

        public List<DoctorDaysOff> FindByDoctorID(int doctorId)
        {
            return _doctorDaysOffRepository.FindByDoctorID(doctorId);
        }
    }
}
