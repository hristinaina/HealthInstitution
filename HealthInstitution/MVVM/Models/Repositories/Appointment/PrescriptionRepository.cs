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
        private string _fileName;
        private List<Prescription> _prescriptions;

        public PrescriptionRepository(string filePath)
        {
            _fileName = filePath;
            _prescriptions = new List<Prescription>();
        }

        public List<Prescription> GetPrescriptions()
        {
            return _prescriptions;
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
            return null;
        }
    }
}
