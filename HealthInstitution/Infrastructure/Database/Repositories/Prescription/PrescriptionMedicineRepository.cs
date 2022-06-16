using HealthInstitution.Core.Services;
using System.Collections.Generic;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Repositories.References
{
    public class PrescriptionMedicineRepository : BaseRepository, IPrescriptionMedicineRepository
    {
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

        public override void LoadFromFile()
        {
            _references = FileService.Deserialize<PrescriptionMedicine>(_fileName);
        }

        public override void SaveToFile()
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

        public List<PrescriptionMedicine> GetPrescriptionMedicines()
        {
            return _references;
        }
    }
}
