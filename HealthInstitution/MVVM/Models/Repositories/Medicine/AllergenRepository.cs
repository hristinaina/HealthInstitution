using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class AllergenRepository
    {
        private string _fileName;
        private List<Allergen> _allergen;
        public List<Allergen> Allergen { get => _allergen; }

        public AllergenRepository(string fileName)
        {
            _fileName = fileName;
            _allergen = new List<Allergen>();
        }

        public void LoadFromFile()
        {
            _allergen = FileService.Deserialize<Allergen>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Allergen>(_fileName, _allergen);
        }
        public Allergen FindByID(int id)
        {
            foreach (Allergen allergen in _allergen)
            {
                if (allergen.Id == id) return allergen;
            }
            return null;
        }

        public List<Allergen> PatientAllergenToAllergen (List<PatientAllergen> patientAllergens)
        {
            List<Allergen> allergens = new();
            foreach (PatientAllergen reference in patientAllergens)
            {
                allergens.Add(FindByID(reference.IngredientId));
            }
            return allergens;
        }

        public List<Allergen> MedicineAllergenToAllergen(List<MedicineAllergen> medicineAllergens)
        {
            List<Allergen> allergens = new();
            foreach (MedicineAllergen reference in medicineAllergens)
            {
                allergens.Add(FindByID(reference.IngredientId));
            }
            return allergens;
        }
    }
}
