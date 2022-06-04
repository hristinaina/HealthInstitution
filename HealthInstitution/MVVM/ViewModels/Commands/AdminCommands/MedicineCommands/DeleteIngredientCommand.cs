using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.MedicineCommands
{
    class DeleteIngredientCommand : BaseCommand
    {
        private AdminMedicineViewModel _model;

        public DeleteIngredientCommand(AdminMedicineViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Institution.Instance().AllergenRepository.DeleteAllergen(_model.SelectedIngredient);

                _model.DialogOpen = false;
                _model.FillIngredientList();
            }
            catch (IngredientInUseException e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
