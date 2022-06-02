using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Exceptions;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Entities
{
    class CreateMedicineService
    {

        private PendingMedicineRepository _repository;

        public CreateMedicineService()
        {
            _repository = Institution.Instance().PendingMedicineRepository;
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
