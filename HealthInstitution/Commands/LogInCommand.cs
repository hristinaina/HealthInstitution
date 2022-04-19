using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.Commands
{
    class LogInCommand : CommandBase
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;

        public LogInCommand()
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new PatientMainViewModel();
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
