using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class AllergenViewModel : BaseViewModel
    {
        private readonly Allergen _allergen;

        public string AllergenName => _allergen.Name;

        public AllergenViewModel(Allergen allergen)
        {
            _allergen = allergen;
        }
    }
}
