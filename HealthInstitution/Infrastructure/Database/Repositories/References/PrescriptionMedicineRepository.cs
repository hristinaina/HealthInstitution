using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories.References
{
    public class PrescriptionMedicineRepository
    {
        private readonly string _fileName;
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

        public PrescriptionMedicine FindByPrescriptionID(int prescriptionId)
        {
            PrescriptionMedicine prescriptionMedicine = new();
            foreach (PrescriptionMedicine reference in _references)
            {
                if (reference.PrescriptionID == prescriptionId)
                    prescriptionMedicine = reference;
            }
            return prescriptionMedicine;
        }

        public void Add(PrescriptionMedicine prescriptionMedicine)
        {
            _references.Add(prescriptionMedicine);
        }
    }
}
