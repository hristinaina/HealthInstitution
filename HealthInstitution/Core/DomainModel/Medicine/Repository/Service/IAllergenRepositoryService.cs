using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IAllergenRepositoryService
    {
        public Allergen FindByID(int id);
        public List<Allergen> PatientAllergenToAllergen(List<PatientAllergen> patientAllergens);
        public List<Allergen> MedicineAllergenToAllergen(List<MedicineAllergen> medicineAllergens);
        public void ChangeName(Allergen allergen, string name);
        public Allergen AddNewAllergen(Allergen allergen);
        public void DeleteAllergen(Allergen allergen);
        public List<Allergen> GetAllergens();
    }
}
