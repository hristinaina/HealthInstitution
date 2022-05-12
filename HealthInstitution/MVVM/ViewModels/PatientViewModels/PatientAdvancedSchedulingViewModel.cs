using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Views.PatientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientAdvancedSchedulingViewModel : BaseViewModel
    { 
        public PatientNavigationViewModel Navigation { get; }

        private readonly Patient _patient;
        public Patient Patient { get => _patient; }
        private Institution _institution;

        public PatientAdvancedSchedulingViewModel()
        {
            Navigation = new PatientNavigationViewModel();

            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
        }
        }
}
