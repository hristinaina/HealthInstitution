using HealthInstitution.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class PendingMedicineRepositoryService : IPendingMedicineRepositoryService
    {
        private readonly IPendingMedicineRepository _pendingMedicineRepository;

        public PendingMedicineRepositoryService(IPendingMedicineRepository pendingMedicineRepository)
        {
            _pendingMedicineRepository = pendingMedicineRepository;
        }

        public PendingMedicine FindByID(int id)
        {
            return _pendingMedicineRepository.FindByID(id);
        }

        private bool IsNameAvailable(PendingMedicine medicine, string name)
        {
            foreach (PendingMedicine m in _pendingMedicineRepository.GetPendingMedicines())
            {
                if (m.Name.Equals(name) && !m.Equals(medicine)) return false;
            }
            return true;
        }

        public PendingMedicine AddNewMedicine(PendingMedicine newMedicine)
        {
            if(!IsNameAvailable(null, newMedicine.Name)) throw new NameNotAvailableException("Name already in use!");
            return _pendingMedicineRepository.AddNewMedicine(newMedicine);
        }

        public PendingMedicine ChangeMedicine(PendingMedicine medicine, string newName, List<Allergen> newIngredients)
        {
            if (newName is null || newName.Equals("")) throw new NameNotAvailableException("Name cannot be empty");
            else if (newIngredients.Count == 0) throw new ZeroIngredientsException("Ingredient list cannot be empty");
            else if (!IsNameAvailable(medicine, newName)) throw new NameNotAvailableException("Name already in use!");
            
            return _pendingMedicineRepository.ChangeMedicine(medicine, newName, newIngredients);
        }
    }
}
