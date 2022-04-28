using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories.References
{
    public class MedicineAllergenRepository
    {
        private readonly string _fileName;
        private List<MedicineAllergen> _references;

        public MedicineAllergenRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<MedicineAllergen>();
        }
        public List<MedicineAllergen> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<MedicineAllergen>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<MedicineAllergen>(_fileName, _references);
        }

        public List<MedicineAllergen> FindByMedicineID(int medicineId)
        {
            List<MedicineAllergen> medicineAllergens = new();
            foreach (MedicineAllergen reference in _references)
            {
                if (reference.MedicineId == medicineId)
                    medicineAllergens.Add(reference);
            }
            return medicineAllergens;
        }
    }
}
