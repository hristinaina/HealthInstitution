using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class PendingMedicineRepository
    {
        private readonly string _fileName;
        private List<PendingMedicine> _pendingMedicines;
        public List<PendingMedicine> PendingMedicines { get => _pendingMedicines; }

        public PendingMedicineRepository(string fileName)
        {
            _fileName = fileName;
            _pendingMedicines = new List<PendingMedicine>();
        }

        public void LoadFromFile()
        {
            _pendingMedicines = FileService.Deserialize<PendingMedicine>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<PendingMedicine>(_fileName, _pendingMedicines);
        }
        public PendingMedicine FindByID(int id)
        {
            foreach (PendingMedicine medicine in _pendingMedicines)
            {
                if (medicine.Id == id) return medicine;
            }
            return null;
        }
    }
}
