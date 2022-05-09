using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories.References
{
    public class ReferralRepository
    {
        private readonly string _fileName;
        private List<Referral> _references;

        public ReferralRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<Referral>();
        }
        public List<Referral> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<Referral>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Referral>(_fileName, _references);
        }

        public List<Referral> FindByPatientID(int patientId)
        {
            List<Referral> refferals = new List<Referral>();
            foreach (Referral reference in _references)
            {
                if (reference.PatientId == patientId)
                    refferals.Add(reference);
            }
            return refferals;
        }

        public void Add(Refferal referral)
        {
            _references.Add(referral);
        }
    }
}
