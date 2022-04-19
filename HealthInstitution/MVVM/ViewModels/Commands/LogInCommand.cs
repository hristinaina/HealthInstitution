using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.Commands
{
    class LogInCommand : CommandBase
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        private readonly LoginViewModel _loginVM;

        public LogInCommand(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            if (_institution.Login(_loginVM.Email, _loginVM.Password))
            {
                _navigationStore.CurrentViewModel = new PatientMainViewModel();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }
    }
}
