using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories.References
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

        public List<Referral> Referrals { get => _references; }

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

        public Referral FindByID(int id)
        {
            foreach (Referral referral in _references)
            {
                if (referral.Id == id) return referral;
            }
            return null;
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

        public void Add(Referral referral)
        {
            _references.Add(referral);
        }

        private bool CheckID(int id)
        {
            foreach (Referral r in _references)
            {
                if (r.Id == id) return false;
            }
            return true;
        }

        public int GetNewID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }
    }
}
