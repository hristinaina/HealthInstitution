using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    public class AllergenItemViewModel : BaseViewModel
    {
        private readonly Allergen _allergen;

        public string AllergenName => _allergen.Name;

        public AllergenItemViewModel(Allergen allergen)
        {
            _allergen = allergen;
            if (_allergen == null)
            {
                _allergen = new Allergen(0, " ");
            }
        }
    }
}
