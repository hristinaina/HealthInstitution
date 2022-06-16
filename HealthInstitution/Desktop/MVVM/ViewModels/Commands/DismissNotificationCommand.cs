using HealthInstitution.Commands;

namespace HealthInstitution.MVVM.ViewModels.Commands
{
    class DismissNotificationCommand : BaseCommand
    {
        private readonly BaseViewModel _viewModel;

        public DismissNotificationCommand(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.HideNotification();
        }
    }
}
