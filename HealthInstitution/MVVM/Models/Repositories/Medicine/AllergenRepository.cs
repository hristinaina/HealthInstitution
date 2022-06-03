using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class AllergenRepository
    {
        private readonly string _fileName;
        private List<Allergen> _allergens;
        public List<Allergen> Allergens { get => _allergens; }

        public AllergenRepository(string fileName)
        {
            _fileName = fileName;
            _allergens = new List<Allergen>();
        }

        public void LoadFromFile()
        {
            _allergens = FileService.Deserialize<Allergen>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Allergen>(_fileName, _allergens);
        }
        public Allergen FindByID(int id)
        {
            foreach (Allergen allergen in _allergens)
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

        private bool CheckID(int id)
        {
            foreach (Allergen m in _allergens)
            {
                if (m.Id == id) return false;
            }

            return true;
        }

        private int GetID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

        private bool IsNameAvailable(Allergen allergen, string name)
        {
            foreach (Allergen a in _allergens)
            {
                if (a.Name.Equals(name) && !a.Equals(allergen)) return false;
            }
            return true;
        }

        private bool isDeletable(Allergen allergen)
        {
            foreach (PendingMedicine medicine in Institution.Instance().PendingMedicineRepository.PendingMedicines)
            {
                foreach (Allergen a in medicine.Ingredients)
                {
                    if (a.Equals(allergen)) return false;
                }
            }

            foreach (Medicine medicine in Institution.Instance().MedicineRepository.Medicine)
            {
                foreach (Allergen a in medicine.Ingredients)
                {
                    if (a.Equals(allergen)) return false;
                }
            }

            return true;
        }

        public void ChangeName(Allergen allergen, string name)
        {
            if (!IsNameAvailable(allergen, name)) throw new NameNotAvailableException("Name already taken");

            allergen.Name = name;
        }

        public Allergen AddNewAllergen(Allergen allergen)
        {
            if (!IsNameAvailable(allergen, allergen.Name)) throw new NameNotAvailableException("Name already in use!");
            
            allergen.Id = GetID();
            _allergens.Add(allergen);
            return allergen;
        }

        public void DeleteAllergen(Allergen allergen)
        {
            if (!isDeletable(allergen)) throw new IngredientInUseException("Ingredient in use!");

            _allergens.Remove(allergen);
        }
    }
}
