using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface ICreateMedicineService
    {
        public PendingMedicine Create(string name, List<Allergen> ingredients);
    }
}