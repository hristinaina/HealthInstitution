using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IReferralRepositoryService
    {
        public Referral FindByID(int id);

        public List<Referral> FindByPatientID(int patientId);

        public void Add(Referral referral);

        public int GetNewID();

        public List<Referral> GetReferrals();
    }
}
