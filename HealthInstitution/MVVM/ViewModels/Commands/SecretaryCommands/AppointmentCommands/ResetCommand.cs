using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands
{
    public class ResetCommand : BaseCommand
    {
        private AppointmentsViewModel _viewModel;

        public ResetCommand(AppointmentsViewModel model)
        {
            _viewModel = model;
        }
        public override void Execute(object parameter)
        {
            _viewModel.SearchPhrase = null;
            _viewModel.FillReferralsList();
        }
    }
}
