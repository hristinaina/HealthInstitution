using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SearchContentCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;

        public SearchContentCommand()
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new PatientSearchViewModel();
        }
    }
}
