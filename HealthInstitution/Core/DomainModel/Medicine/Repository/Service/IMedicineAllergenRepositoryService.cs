using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IMedicineAllergenRepositoryService
    {
        public List<MedicineAllergen> FindByMedicineID(int medicineId);
        
        public void Add(Medicine medicine);

        public List<MedicineAllergen> GetMedicineAllergens();
    }
}
