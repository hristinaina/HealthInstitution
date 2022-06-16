using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Core.Repository;

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
                new AllergenRepositoryService().DeleteAllergen(_model.SelectedIngredient);

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
