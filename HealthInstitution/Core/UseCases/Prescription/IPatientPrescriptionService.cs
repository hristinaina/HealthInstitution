using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    interface IPatientPrescriptionService
    {
        public List<Prescription> GetUpgoingPrescriptions();
        public List<Prescription> GetAllPrescriptions();
        public DateTime GetPrescriptionDate(Prescription p);
    }
}
