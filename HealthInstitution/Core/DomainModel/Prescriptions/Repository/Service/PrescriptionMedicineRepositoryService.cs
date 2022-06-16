using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class PrescriptionMedicineRepositoryService : IPrescriptionMedicineRepositoryService
    {
        private readonly IPrescriptionMedicineRepository _prescriptionMedicineRepository;

        public void Add(PrescriptionMedicine prescriptionMedicine)
        {
            _prescriptionMedicineRepository.Add(prescriptionMedicine);
        }

        public PrescriptionMedicine FindByPrescriptionID(int prescriptionId)
        {
            return _prescriptionMedicineRepository.FindByPrescriptionID(prescriptionId);
        }
    }
}
