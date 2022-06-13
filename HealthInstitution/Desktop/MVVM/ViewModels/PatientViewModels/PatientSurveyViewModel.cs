using HealthInstitution.Core;
using HealthInstitution.MVVM.Views.PatientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    class PatientSurveyViewModel : BaseViewModel
    {
        private Institution _institution;
        private readonly Patient _patient;
        public PatientNavigationViewModel Navigation { get; }

        public PatientSurveyViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            Navigation = new PatientNavigationViewModel();
            // ..............
        }

    }
}

