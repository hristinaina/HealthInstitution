using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IPrescriptionMedicineRepositoryService
    {
        public PrescriptionMedicine FindByPrescriptionID(int prescriptionId);

        public void Add(PrescriptionMedicine prescriptionMedicine);
    }
}
