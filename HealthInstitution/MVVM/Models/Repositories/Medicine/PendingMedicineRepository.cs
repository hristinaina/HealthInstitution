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
        private string _fileName;
        private List<PendingMedicine> _pendingMedicines;
        public List<PendingMedicine> PendingMedicines { get => this._pendingMedicines; }

        public PendingMedicineRepository(string fileName)
        {
            this._fileName = fileName;
            this._pendingMedicines = new List<PendingMedicine>();
        }

        public void LoadFromFile()
        {
            _pendingMedicines = FileService.Deserialize<PendingMedicine>(this._fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<PendingMedicine>(this._fileName, this._pendingMedicines);
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
