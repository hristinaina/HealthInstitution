using HealthInstitution.Core;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public interface IDoctorSearch
    {
        public List<Core.Doctor> SearchForDoctor(Core.Doctor doctor);

    }
}
