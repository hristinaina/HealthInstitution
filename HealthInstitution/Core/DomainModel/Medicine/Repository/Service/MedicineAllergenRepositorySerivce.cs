using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    class MedicineAllergenRepositorySerivce : IMedicineAllergenRepositoryService
    {
        private readonly IMedicineAllergenRepository _medicineAllergenRepository;

        public MedicineAllergenRepositorySerivce(IMedicineAllergenRepository medicineAllergenRepository)
        {
            _medicineAllergenRepository = medicineAllergenRepository;
        }

        public List<MedicineAllergen> FindByMedicineID(int medicineId)
        {
            return _medicineAllergenRepository.FindByMedicineID(medicineId);
        }

        public void Add(Medicine medicine)
        {
            _medicineAllergenRepository.Add(medicine);
        }
    }
}
