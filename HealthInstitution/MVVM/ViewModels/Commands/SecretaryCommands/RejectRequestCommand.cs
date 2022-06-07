using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class RejectRequestCommand : BaseCommand
    {
        private readonly Institution _institution;
        private DoctorDaysOffViewModel _viewModel;

        public RejectRequestCommand(DoctorDaysOffViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            MessageBox.Show("The request has been rejected! Notification sent to doctor.");
            // izmjeni status request-a
            // posalji notifikaciju doktoru
            _viewModel.FillRequestsList();
        }
    }
}
