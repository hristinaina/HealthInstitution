using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    class MedicineRepositoryService : IMedicineRepositoryService
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineRepositoryService()
        {
            _medicineRepository = Institution.Instance().MedicineRepository;
        }

        public Medicine FindByID(int id)
        {
            return _medicineRepository.FindByID(id);
        }

        public Medicine PrescriptionMedicineToMedicine(PrescriptionMedicine prescriptionMedicine)
        {
            return _medicineRepository.PrescriptionMedicineToMedicine(prescriptionMedicine);
        }

        public void Add(Medicine medicine)
        {
            _medicineRepository.Add(medicine);
        }

        public Medicine AddNewMedicine(Medicine newMedicine)
        {
            return _medicineRepository.AddNewMedicine(newMedicine);
        }

        public List<Medicine> GetMedicines()
        {
            return _medicineRepository.GetMedicines();
        }
    }
}
