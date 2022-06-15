using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands
{
    class LogOutCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;

        public LogOutCommand()
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new LogInViewModel();
        }
    }
}
