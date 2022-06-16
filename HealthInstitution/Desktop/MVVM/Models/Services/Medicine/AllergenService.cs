using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    public class AllergenService
    {
        private Allergen _allergen;
        private IAllergenRepositoryService _allergens;
        private IMedicineRepositoryService _medicine;
        private IPendingMedicineRepositoryService _pendingMedicine;

        public AllergenService(Allergen a)
        {
            _allergen = a;
            _allergens = new AllergenRepositoryService();
            _medicine = new MedicineRepositoryService();
            _pendingMedicine = new PendingMedicineRepositoryService();
        }

        public bool IsNameAvailable(string name)
        {
            foreach (Allergen a in _allergens.GetAllergens())
            {
                if (a.Name.Equals(name) && !a.Equals(_allergen)) return false;
            }
            return true;
        }

        public bool isDeletable()
        {
            foreach (PendingMedicine medicine in _pendingMedicine.GetPendingMedicines())
            {
                foreach (Allergen a in medicine.Ingredients)
                {
                    if (a.Equals(_allergen)) return false;
                }
            }

            foreach (Medicine medicine in _medicine.GetMedicines())
            {
                foreach (Allergen a in medicine.Ingredients)
                {
                    if (a.Equals(_allergen)) return false;
                }
            }

            return true;
        }
    }
}