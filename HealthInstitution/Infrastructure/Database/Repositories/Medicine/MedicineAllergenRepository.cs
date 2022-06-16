using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories.References
{
    public class MedicineAllergenRepository
    {
        private readonly string _fileName;
        private List<MedicineAllergen> _allergensInMedicine;

        public List<MedicineAllergen> AllergensInMedicine
        {
            get => _allergensInMedicine;
            set => _allergensInMedicine = value;
        }

        public MedicineAllergenRepository(string fileName)
        {
            _fileName = fileName;
            _allergensInMedicine = new List<MedicineAllergen>();
        }
        public List<MedicineAllergen> GetReferences()
        {
            return _allergensInMedicine;
        }

        public void LoadFromFile()
        {
            _allergensInMedicine = FileService.Deserialize<MedicineAllergen>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<MedicineAllergen>(_fileName, _allergensInMedicine);
        }

        public List<MedicineAllergen> FindByMedicineID(int medicineId)
        {
            List<MedicineAllergen> medicineAllergens = new();
            foreach (MedicineAllergen reference in _allergensInMedicine)
            {
                if (reference.MedicineId == medicineId)
                    medicineAllergens.Add(reference);
            }
            return medicineAllergens;
        }

        public void Add(Medicine medicine)
        {
            foreach(Allergen allergen in medicine.Ingredients)
            {
                MedicineAllergen medicineAllergen = new MedicineAllergen(medicine.ID, allergen.ID);
                _allergensInMedicine.Add(medicineAllergen);
            }
        }
    }
}
