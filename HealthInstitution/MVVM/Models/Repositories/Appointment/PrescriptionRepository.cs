using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class PrescriptionRepository
    {
        private readonly string _fileName;
        private List<Prescription> _prescriptions;

        public List<Prescription> Prescriptions { get => _prescriptions; }
        public PrescriptionRepository(string filePath)
        {
            _fileName = filePath;
            _prescriptions = new List<Prescription>();
        }

        public void LoadFromFile()
        {
            _prescriptions = FileService.Deserialize<Prescription>(_fileName);

        }

        public void SaveToFile()
        {
            FileService.Serialize<Prescription>(_fileName, _prescriptions);
        }
        public Prescription FindByID(int id)
        {
            foreach (Prescription perscription in _prescriptions)
            {
                if (perscription.ID == id) return perscription;
            }
            return new Prescription(NewId());
        }

        public int NewId()
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
