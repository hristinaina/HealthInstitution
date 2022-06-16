using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IDoctorRepositoryService
    {
        public Doctor FindByID(int id);

        public List<Doctor> GetGeneralPractitioners();

        public Doctor FindDoctorBySpecialization(Specialization specialization);

        public List<Doctor> GetDoctors();
    }
}
