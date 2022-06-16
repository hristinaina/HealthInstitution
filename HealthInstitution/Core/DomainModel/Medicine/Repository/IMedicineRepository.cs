using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IMedicineRepository : IRepository
    {
        public Medicine FindByID(int id);

        public Medicine PrescriptionMedicineToMedicine(PrescriptionMedicine prescriptionMedicine);

        public void Add(Medicine medicine);

        public Medicine AddNewMedicine(Medicine newMedicine);

        public List<Medicine> GetMedicines();
    }
}