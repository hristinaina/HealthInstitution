using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class PrescriptionRepository : BaseRepository, IPrescriptionRepository
    {
        private List<Prescription> _prescriptions;

        public List<Prescription> Prescriptions { get => _prescriptions; }
        public PrescriptionRepository(string filePath)
        {
            _fileName = filePath;
            _prescriptions = new List<Prescription>();
        }

        public override void LoadFromFile()
        {
            _prescriptions = FileService.Deserialize<Prescription>(_fileName);

        }

        public override void SaveToFile()
        {
            FileService.Serialize<Prescription>(_fileName, _prescriptions);
        }
        public Prescription FindByID(int id)
        {
            foreach (Prescription perscription in _prescriptions)
            {
                if (perscription.ID == id) return perscription;
            }
            return new Prescription(GetID());
        }

        public int GetID()
        {
            if (_prescriptions.Count == 0)
            {
                return 1;
            }
            return _prescriptions.Max(x => x.ID) + 1;
        }

        public void Add(Prescription prescription)
        {
            _prescriptions.Add(prescription);
        }
    }
}
