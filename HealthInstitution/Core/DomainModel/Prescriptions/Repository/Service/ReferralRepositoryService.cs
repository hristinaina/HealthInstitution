using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class ReferralRepositoryService : IReferralRepositoryService
    {
        private readonly IReferralRepository _referralRepository;

        public ReferralRepositoryService()
        {
            _referralRepository = Institution.Instance().ReferralRepository;
        }

        public void Add(Referral referral)
        {
            _referralRepository.Add(referral);
        }

        public Referral FindByID(int id)
        {
            return _referralRepository.FindByID(id);
        }

        public List<Referral> FindByPatientID(int patientId)
        {
            return _referralRepository.FindByPatientID(patientId);
        }

        public int GetNewID()
        {
            return _referralRepository.GetNewID();
        }

        public List<Referral> GetReferrals()
        {
            return _referralRepository.GetReferrals();
        }
    }
}
