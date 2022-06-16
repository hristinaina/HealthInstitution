using System.Collections.Generic;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class MedicineRepository : BaseRepository
    {
        private List<Medicine> _medicines;

        public List<Medicine> Medicines { get => _medicines; }
        public MedicineRepository(string fileName)
        {
            _fileName = fileName;
            _medicines = new List<Medicine>();
        }

        public override void LoadFromFile()
        {
            _medicines = FileService.Deserialize<Medicine>(_fileName);
        }

        public override void SaveToFile()
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
        
        private bool CheckID(int id)
        {
            foreach (Medicine m in _medicines)
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
            _medicines.Add(newMedicine);

            return newMedicine;
        }
    }
}
