using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    public interface IDoctorPrescriptionService
    {
        public bool CreatePrescription(Prescription prescription, Examination examination);
    }
}
