using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.UseCases
{
    public interface ISecretaryReferralService
    {
        public void RemoveReferral(int referralId);

        public void UseReferral(int referralId, DateTime datetime);

        public void RemoveReferralsOfDeletedPatients();

        public List<Referral> SearchMatchingReferrals(string phrase);
    }
}
