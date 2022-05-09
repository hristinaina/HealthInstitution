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
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class ApproveCommand : BaseCommand
    {
        private readonly Institution _institution;
        private AppointmentRequestsViewModel _viewModel;

        public ApproveCommand(AppointmentRequestsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            ExaminationChange change = Institution.Instance().ExaminationChangeRepository.FindByID(_viewModel.SelectedRequestId);
            SecretaryService.ApproveChange(change);
            _viewModel.FillRequestsList();
        }
    }
}
