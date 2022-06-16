using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    public class AllergenValidationService
    {
        private IAllergenRepositoryService _allergens;
        private IMedicineRepositoryService _medicine;
        private IPendingMedicineRepositoryService _pendingMedicine;

        public AllergenValidationService()
        {
            _allergens = new AllergenRepositoryService();
            _medicine = new MedicineRepositoryService();
            _pendingMedicine = new PendingMedicineRepositoryService();
        }

        public bool IsNameAvailable(string name, Allergen allergen)
        {
            foreach (Allergen a in _allergens.GetAllergens())
            {
                if (a.Name.Equals(name) && !a.Equals(allergen)) return false;
            }
            return true;
        }

        public bool IsDeletable(Allergen allergen)
        {
            foreach (PendingMedicine medicine in _pendingMedicine.GetPendingMedicines())
            {
                foreach (Allergen a in medicine.Ingredients)
                {
                    if (a.Equals(allergen)) return false;
                }
            }

            foreach (Medicine medicine in _medicine.GetMedicines())
            {
                foreach (Allergen a in medicine.Ingredients)
                {
                    if (a.Equals(allergen)) return false;
                }
            }

            return true;
        }
    }
}