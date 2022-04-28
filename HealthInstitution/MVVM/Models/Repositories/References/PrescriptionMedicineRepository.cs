using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories.References
{
    public class PrescriptionMedicineRepository
    {
        private string _fileName;
        private List<PrescriptionMedicine> _references;

        public PrescriptionMedicineRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<PrescriptionMedicine>();
        }
        public List<PrescriptionMedicine> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<PrescriptionMedicine>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<PrescriptionMedicine>(_fileName, _references);
        }

        public List<PrescriptionMedicine> FindByPrescriptionID(int prescriptionId)
        {
            List<PrescriptionMedicine> prescriptionMedicines = new();
            foreach (PrescriptionMedicine reference in _references)
            {
                if (reference.PrescriptionID == prescriptionId)
                    prescriptionMedicines.Add(reference);
            }
            return prescriptionMedicines;
        }
    }
}
