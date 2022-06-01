using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class MedicineRepository
    {
        private readonly string _fileName;
        private List<Medicine> _medicines;

        public List<Medicine> Medicines { get => _medicines; }
        public MedicineRepository(string fileName)
        {
            _fileName = fileName;
            _medicines = new List<Medicine>();
        }

        public void LoadFromFile()
        {
            _medicines = FileService.Deserialize<Medicine>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Medicine>(_fileName, _medicines);
        }
        public Medicine FindByID(int id)
        {
            foreach (Medicine medicine in _medicines)
            {
                if (medicine.ID == id) return medicine;
            }
            return null;
        }

        public Medicine PrescriptionMedicineToMedicine(PrescriptionMedicine prescriptionMedicine)
        {
            return FindByID(prescriptionMedicine.MedicineId);
        }

        public void Add(Medicine medicine)
        {
            _medicines.Add(medicine);
        }
    }
}
