using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Services.DoctorServices;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class RejectRequestCommand : BaseCommand
    {
        private readonly Institution _institution;
        private DoctorDaysOffViewModel _viewModel;
        private DayOffService _service;

        public RejectRequestCommand(DoctorDaysOffViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new DayOffService();
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            MessageBox.Show("The request has been rejected! Notification sent to doctor.");
            _service.AcceptRequest(_viewModel.SelectedRequest.ID); 
            _viewModel.FillRequestsList();
        }
    }
}
