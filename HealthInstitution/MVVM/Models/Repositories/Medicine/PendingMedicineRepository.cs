using System;
using System.Collections.Generic;
using System.Drawing;
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
                if (medicine.ID == id) return medicine;
            }
            return null;
        }

        private bool CheckID(int id)
        {
            foreach (PendingMedicine m in _pendingMedicines)
            {
                if (m.ID == id) return false;
            }

            foreach (Medicine m in Institution.Instance().MedicineRepository.Medicine)
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

        public PendingMedicine AddNewMedicine(PendingMedicine newMedicine)
        {
            newMedicine.ID = GetID();
            _pendingMedicines.Add(newMedicine);

            return newMedicine;
        }
    }
}