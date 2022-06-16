using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IPrescriptionMedicineRepository : IRepository
    {
        public PrescriptionMedicine FindByPrescriptionID(int prescriptionId);

        public void Add(PrescriptionMedicine prescriptionMedicine);
        public List<PrescriptionMedicine> GetPrescriptionMedicines();
    }
}