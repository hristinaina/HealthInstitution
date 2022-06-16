using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class DoctorRepositoryService : IDoctorRepositoryService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorRepositoryService()
        {
            _doctorRepository = Institution.Instance().DoctorRepository;
        }

        public Doctor FindByID(int id)
        {
            return _doctorRepository.FindByID(id);
        }

        public Doctor FindDoctorBySpecialization(Specialization specialization)
        {
            return _doctorRepository.FindDoctorBySpecialization(specialization);
        }

        public List<Doctor> GetDoctors()
        {
            return _doctorRepository.GetDoctors();
        }

        public List<Doctor> GetGeneralPractitioners()
        {
            return _doctorRepository.GetGeneralPractitioners();
        }
    }
}
