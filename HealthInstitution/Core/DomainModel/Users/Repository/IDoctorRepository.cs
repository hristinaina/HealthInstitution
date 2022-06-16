using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IDoctorRepository : IRepository
    {
        public Doctor FindByID(int id);

        public List<Doctor> GetGeneralPractitioners();

        public Doctor FindDoctorBySpecialization(Specialization specialization);

        public List<Doctor> GetDoctors();
    }
}