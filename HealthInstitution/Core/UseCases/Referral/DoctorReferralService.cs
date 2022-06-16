using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories.References;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    class DoctorReferralService
    {
        private IReferralRepositoryService _referralService;

        public DoctorReferralService()
        {
            _referralService = new ReferralRepositoryService();
        }

        public bool CreateReferral(int doctorId, int patientId, Specialization specialization)
        {
            int id = _referralService.GetNewID();
            Referral referral = new Referral(id, patientId, doctorId, specialization);
            _referralService.Add(referral);
            return true;
        }
    }
}
