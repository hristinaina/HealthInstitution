using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class MedicineRepository
    {
        private readonly string _fileName;
        private List<Medicine> _medicine;

        public List<Medicine> Medicine { get => _medicine; }
        public MedicineRepository(string fileName)
        {
            _fileName = fileName;
            _medicine = new List<Medicine>();
        }

        public void LoadFromFile()
        {
            _medicine = FileService.Deserialize<Medicine>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Medicine>(_fileName, _medicine);
        }

        public Medicine FindByID(int id)
        {
            foreach (Medicine medicine in _medicine)
            {
                if (medicine.ID == id) return medicine;
            }
            return null;
        }

        public Medicine PrescriptionMedicineToMedicine(PrescriptionMedicine prescriptionMedicine)
        {
            return FindByID(prescriptionMedicine.MedicineId);
        }

        private bool CheckID(int id)
        {
            foreach (Medicine m in _medicine)
            {
                if (m.ID == id) return false;
            }

            return true;
        }

        private int GetID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

        public Medicine AddNewMedicine(Medicine newMedicine)
        {
            newMedicine.ID = GetID();
            _medicine.Add(newMedicine);

            return newMedicine;
        }
    }
}
