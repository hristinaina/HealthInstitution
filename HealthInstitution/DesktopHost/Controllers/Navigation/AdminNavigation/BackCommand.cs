using HealthInstitution.Commands;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands
{
    class BackCommand : BaseCommand
    {
        public BackCommand()
        {
        }

        public override void Execute(object parameter)
        {
            NavigationStore.Instance().CurrentViewModel = new AdminRenovationViewModel();
        }
    }
}
