using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class RejectCommand : BaseCommand
    {

        private readonly Institution _institution;
        private AppointmentRequestsViewModel _viewModel;

        public RejectCommand(AppointmentRequestsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            ExaminationChange change = Institution.Instance().ExaminationChangeRepository.FindByID(_viewModel.SelectedRequestId);
            SecretaryService.RejectChange(change);
            _viewModel.FillRequestsList();
        }
    }
}
