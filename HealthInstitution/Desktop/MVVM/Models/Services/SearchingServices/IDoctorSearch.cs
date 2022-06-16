using HealthInstitution.Core;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public interface IDoctorSearch
    {
        public List<Doctor> SearchForDoctor(Doctor doctor);

    }
}
