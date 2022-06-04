using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Repositories.References;

namespace HealthInstitution.MVVM.Models.Services
{
    class DoctorReferralService
    {
        private ReferralRepository _referralRepository;

        public DoctorReferralService()
        {
            _referralRepository = Institution.Instance().ReferralRepository;
        }

        public bool CreateReferral(int doctorId, int patientId, Specialization specialization)
        {
            int id = Institution.Instance().ReferralRepository.GetNewID();
            Referral referral = new Referral(id, patientId, doctorId, specialization);
            _referralRepository.Add(referral);
            return true;
        }
    }
}
