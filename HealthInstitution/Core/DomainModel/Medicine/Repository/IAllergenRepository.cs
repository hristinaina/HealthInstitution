using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IAllergenRepository
    {
        public Allergen FindByID(int id);

        public List<Allergen> PatientAllergenToAllergen(List<PatientAllergen> patientAllergens);

        public List<Allergen> MedicineAllergenToAllergen(List<MedicineAllergen> medicineAllergens);

        public void ChangeName(Allergen allergen, string name);

        public Allergen AddNewAllergen(Allergen allergen);

        public void DeleteAllergen(Allergen allergen);
    }
}