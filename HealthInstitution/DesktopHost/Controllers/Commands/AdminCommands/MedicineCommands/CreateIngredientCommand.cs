using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.MedicineCommands
{
    class CreateIngredientCommand :BaseCommand
    {

        private AdminMedicineViewModel _model;

        public CreateIngredientCommand(AdminMedicineViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            try
            {
                new AllergenRepositoryService().AddNewAllergen(new Allergen(_model.NewIngredientName));

                _model.DialogOpen = false;
                _model.FillIngredientList();
            }
            catch (NameNotAvailableException e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
