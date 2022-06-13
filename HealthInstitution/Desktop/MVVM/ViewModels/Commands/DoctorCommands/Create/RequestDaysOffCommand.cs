using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class RequestDaysOffCommand : BaseCommand
    {
        private DoctorDaysOffViewModel _viewModel;

        public RequestDaysOffCommand(DoctorDaysOffViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
        }
    }
}
