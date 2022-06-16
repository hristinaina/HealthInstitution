using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    public interface IDoctorReferralService
    {
        public bool CreateReferral(int doctorId, int patientId, Specialization specialization);
    }
}
