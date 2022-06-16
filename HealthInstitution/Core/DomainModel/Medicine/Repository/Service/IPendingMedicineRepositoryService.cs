using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IPendingMedicineRepositoryService
    {
        public PendingMedicine FindByID(int id);
        public PendingMedicine AddNewMedicine(PendingMedicine newMedicine);
        public PendingMedicine ChangeMedicine(PendingMedicine medicine, string newName, List<Allergen> newIngredients);
    }
}
