using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.MedicineCommands
{
    class CreateMedicineCommand : BaseCommand
    {
        private AdminMedicineViewModel _model;

        public CreateMedicineCommand(AdminMedicineViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            List<Allergen> ingredients = new List<Allergen>();
            foreach (IngredientListItemViewModel i in _model.Ingredients)
            {
                if (i.Selected)
                {
                    ingredients.Add(i.Ingredient);
                }
            }

            try
            {
                CreateMedicineService service = new CreateMedicineService();
                PendingMedicine m = service.Create(_model.NewMedicineName, ingredients);

                foreach (Allergen i in ingredients)
                {
                    Institution.Instance().MedicineAllergenRepository.AllergensInMedicine.Add(new MedicineAllergen(m.ID, i.Id));
                }

                _model.DialogOpen = false;

                _model.FillMedicineList();
            }
            catch (EmptyNameException e)
            {
                _model.ShowMessage(e.Message);
            }
            catch (ZeroIngredientsException e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
