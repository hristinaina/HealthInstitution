using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IMedicineAllergenRepository : IRepository
    {
        public List<MedicineAllergen> FindByMedicineID(int medicineId);

        public void Add(Medicine medicine);

        public List<MedicineAllergen> GetMedicineAllergens();
    }
}