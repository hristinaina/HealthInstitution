using HealthInstitution.Core;
using HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    class PatientSurveyViewModel : BaseViewModel
    {
        private readonly Institution _institution;
        private readonly Patient _patient;
        public PatientNavigationViewModel Navigation { get; }

        private int _service;
        private int _hygiene;
        private int _satisfaction;
        private int _suggestion;
        private string _comment;

        public int Service { get => _service; set { _service = value; } }
        public int Hygiene { get => _hygiene; set { _hygiene = value; } }
        public int Satisfaction { get => _satisfaction; set { _satisfaction = value; } }
        public int Suggestion { get => _suggestion; set { _suggestion = value; } }
        public string Comment { get => _comment; set { _comment = value; OnPropertyChanged(nameof(Comment)); } }

        public ICommand Check { get; set; }
        public ICommand Submit { get; set; }

        public PatientSurveyViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            Navigation = new PatientNavigationViewModel();
            Comment = "";
            Check = new CheckCommand(this);
            Submit = new SubmitSurveyCommand(this);
        }

        internal void ResetReview()
        {
            Service = 0;
            Hygiene = 0;
            Satisfaction = 0;
            Suggestion = 0;
            Comment = "";
        }
    }
}

