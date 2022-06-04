using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class MedicineListItemViewModel : BaseViewModel
    {
        private PendingMedicine _medicine;

        public PendingMedicine Medicine { get => _medicine; set => _medicine = value; }

        public string ID => _medicine.ID.ToString();
        public string Name => _medicine.Name;
        public string State => _medicine.State.ToString();

        public string Description => _medicine.RevisionDescription;

        public List<IngredientListItemViewModel> MedicineIngredients;

        //public MedicineListItemViewModel(Medicine medicine)
        //{
        //    _medicine = medicine;
        //}

        public MedicineListItemViewModel(PendingMedicine medicine)
        {
            _medicine = medicine;
            MedicineIngredients = new List<IngredientListItemViewModel>();

            foreach (Allergen i in medicine.Ingredients)
            {
                MedicineIngredients.Add(new IngredientListItemViewModel(i));
            }
        }
    }
}
