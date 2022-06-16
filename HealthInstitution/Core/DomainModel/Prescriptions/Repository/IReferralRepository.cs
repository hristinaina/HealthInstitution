using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IReferralRepository :IRepository
    {
        public Referral FindByID(int id);

        public List<Referral> FindByPatientID(int patientId);

        public void Add(Referral referral);

        public int GetNewID();

        public List<Referral> GetReferrals();
    }
}