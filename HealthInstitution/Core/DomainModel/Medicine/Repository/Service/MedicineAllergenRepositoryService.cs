using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    class MedicineAllergenRepositoryService : IMedicineAllergenRepositoryService
    {
        private readonly IMedicineAllergenRepository _medicineAllergenRepository;

        public MedicineAllergenRepositoryService()
        {
            _medicineAllergenRepository = Institution.Instance().MedicineAllergenRepository;
        }

        public List<MedicineAllergen> FindByMedicineID(int medicineId)
        {
            return _medicineAllergenRepository.FindByMedicineID(medicineId);
        }

        public void Add(Medicine medicine)
        {
            _medicineAllergenRepository.Add(medicine);
        }

        public List<MedicineAllergen> GetMedicineAllergens()
        {
            return _medicineAllergenRepository.GetMedicineAllergens();
        }
    }
}
