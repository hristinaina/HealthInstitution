using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IMedicineRepositoryService
    {
        public Medicine FindByID(int id);
        public Medicine PrescriptionMedicineToMedicine(PrescriptionMedicine prescriptionMedicine);
        public void Add(Medicine medicine);
        public Medicine AddNewMedicine(Medicine newMedicine);
        public List<Medicine> GetMedicines();
    }
}
