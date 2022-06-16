using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core
{
    class CreateMedicineService
    {

        private IPendingMedicineRepositoryService _repository;

        public CreateMedicineService()
        {
            _repository = new PendingMedicineRepositoryService();
        }
        public PendingMedicine Create(string name, List<Allergen> ingredients)
        {
            if (ingredients.Count == 0) throw new ZeroIngredientsException("At least one ingredient must be selected");
            else if (name is null || name.Equals("")) throw new EmptyNameException("Name must be entered");
            
            PendingMedicine m = _repository.AddNewMedicine(new PendingMedicine(name, ingredients, State.ON_HOLD));
            return m;
        }

    }
}
