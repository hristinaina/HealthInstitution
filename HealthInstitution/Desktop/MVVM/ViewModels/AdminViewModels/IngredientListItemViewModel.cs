using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class IngredientListItemViewModel : BaseViewModel
    {
        private Allergen _ingredient;
        private readonly AdminMedicineViewModel _model;

        public Allergen Ingredient { get => _ingredient; set => _ingredient = value; }

        public string Name
        {
            get => _ingredient.Name;
            set
            {
                try
                {
                    Institution.Instance().AllergenRepository.ChangeName(_ingredient, value);
                }
                catch (NameNotAvailableException e)
                {
                    _model.ShowMessage(e.Message);
                }
            }
        }

        public bool Selected { get; set; }

        //public MedicineListItemViewModel(Medicine medicine)
        //{
        //    _ingredient = medicine;
        //}

        public IngredientListItemViewModel(Allergen allergen)
        {
            _ingredient = allergen;
            Selected = false;
        }

        public IngredientListItemViewModel(Allergen allergen, AdminMedicineViewModel model)
        {
            _ingredient = allergen;
            _model = model;
            Selected = false;
        }
    }
}
