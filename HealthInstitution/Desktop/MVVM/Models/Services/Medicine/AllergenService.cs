using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;

namespace HealthInstitution.Core.Services
{
    public class AllergenService
    {
        private Allergen _allergen;
        private AllergenRepository _allergens;
        private MedicineRepository _medicine;
        private PendingMedicineRepository _pendingMedicine;

        public AllergenService(Allergen a)
        {
            _allergen = a;
            _allergens = Institution.Instance().AllergenRepository;
            _medicine = Institution.Instance().MedicineRepository;
            _pendingMedicine = Institution.Instance().PendingMedicineRepository;
        }

        public bool IsNameAvailable(string name)
        {
            foreach (Allergen a in _allergens.Allergens)
            {
                if (a.Name.Equals(name) && !a.Equals(_allergen)) return false;
            }
            return true;
        }

        public bool isDeletable()
        {
            foreach (PendingMedicine medicine in _pendingMedicine.PendingMedicines)
            {
                foreach (Allergen a in medicine.Ingredients)
                {
                    if (a.Equals(_allergen)) return false;
                }
            }

            foreach (Medicine medicine in _medicine.Medicines)
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