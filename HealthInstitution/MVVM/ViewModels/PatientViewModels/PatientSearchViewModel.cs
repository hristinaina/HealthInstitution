using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientSearchViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Patient _patient;
        public PatientNavigationViewModel Navigation { get; }


        public PatientSearchViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            Navigation = new PatientNavigationViewModel();

            // ..............
        }

    }
}
