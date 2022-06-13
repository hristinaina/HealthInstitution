using HealthInstitution.Core;
using HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    class PatientSurveyViewModel : BaseViewModel
    {
        private Institution _institution;
        private readonly Patient _patient;
        public PatientNavigationViewModel Navigation { get; }

        private int _service;
        private int _hygene;
        private int _satisfaction;
        private int _suggestion;
        private string _comment;

        public int Service { get => _service; set { _service = value; } }
        public int Hygene { get => _hygene; set { _hygene = value; } }
        public int Satisfacion { get => _satisfaction; set { _satisfaction = value; } }
        public int Suggestion { get => _suggestion; set { _suggestion = value; } }
        public string Comment { get => _comment; set { _comment = value; } }

        public ICommand Check { get; set; }
        public ICommand Submit { get; set; }

        public PatientSurveyViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            Navigation = new PatientNavigationViewModel();
            Check = new CheckCommand(this);
        }

    }
}

