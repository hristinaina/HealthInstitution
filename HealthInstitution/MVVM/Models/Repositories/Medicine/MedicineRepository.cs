using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class MedicineRepository
    {
        private string _fileName;
        private List<Medicine> _medicine;

        public List<Medicine> Medicine { get => this._medicine; }
        public MedicineRepository(string fileName)
        {
            this._fileName = fileName;
            this._medicine = new List<Medicine>();
        }

        public void LoadFromFile()
        {
            _medicine = FileService.Deserialize<Medicine>(this._fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Medicine>(this._fileName, this._medicine);
        }
        public Medicine FindByID(int id)
        {
            foreach (Medicine medicine in _medicine)
            {
                if (medicine.ID == id) return medicine;
            }
            return null;
        }
    }
}
