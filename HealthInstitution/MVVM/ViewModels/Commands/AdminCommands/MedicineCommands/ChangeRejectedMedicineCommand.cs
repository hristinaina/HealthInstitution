using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.MedicineCommands
{
    class ChangeRejectedMedicineCommand : BaseCommand
    {
        private AdminMedicineViewModel _model;

        public ChangeRejectedMedicineCommand(AdminMedicineViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            string name = _model.SelectedName;

            List<Allergen> newIngredients = new List<Allergen>();

            foreach (IngredientListItemViewModel ingredient in _model.Ingredients)
            {
                if (ingredient.Selected) newIngredients.Add(ingredient.Ingredient);
            }

            try
            {
                PendingMedicine m = Institution.Instance().PendingMedicineRepository
                    .ChangeMedicine(_model.SelectedMedicine.Medicine, _model.SelectedName, newIngredients);

                m.State = State.ON_HOLD;

                _model.FillMedicineList();
                _model.DialogOpen = false;
            }
            catch (NameNotAvailableException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (ZeroIngredientsException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (NoChangeException e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
