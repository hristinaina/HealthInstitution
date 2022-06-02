using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class IngredientListItemViewModel : BaseViewModel
    {
        private Allergen _ingredient;

        public Allergen Ingredient { get => _ingredient; set => _ingredient = value; }

        public string Name => _ingredient.Name;

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
    }
}
