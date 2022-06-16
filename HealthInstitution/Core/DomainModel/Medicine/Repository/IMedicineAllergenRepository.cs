using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IMedicineAllergenRepository
    {
        public List<MedicineAllergen> FindByMedicineID(int medicineId);

        public void Add(Medicine medicine);
    }
}