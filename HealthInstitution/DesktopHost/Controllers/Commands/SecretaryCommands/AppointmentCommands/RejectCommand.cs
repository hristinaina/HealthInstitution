using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class RejectCommand : BaseCommand
    {

        private readonly Institution _institution;
        private AppointmentRequestsViewModel _viewModel;
        private readonly ExaminationChangeService _service;

        public RejectCommand(AppointmentRequestsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new ExaminationChangeService();
        }

        public override void Execute(object parameter)
        {
            _service.RejectChange(_viewModel.SelectedRequestId);
            string message = "This request has been successfully rejected.";
            MessageBox.Show(message);
            _viewModel.FillRequestsList();
        }
    }
}
