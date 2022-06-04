using HealthInstitution.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands
{
    class DismissNotificationCommand : BaseCommand
    {
        private BaseViewModel _viewModel;

        public DismissNotificationCommand(BaseViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.hideNotification();
        }
    }
}
