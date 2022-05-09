using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories.References
{
    public class RefferalRepository
    {
        private readonly string _fileName;
        private List<Refferal> _references;

        public RefferalRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<Refferal>();
        }
        public List<Refferal> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<Refferal>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Refferal>(_fileName, _references);
        }

        public List<Refferal> FindByPatientID(int patientId)
        {
            List<Refferal> refferals = new List<Refferal>();
            foreach (Refferal reference in _references)
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
