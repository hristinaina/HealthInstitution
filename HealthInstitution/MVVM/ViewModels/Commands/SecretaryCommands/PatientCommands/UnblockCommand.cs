using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class UnblockCommand : BaseCommand
    {
        private readonly Institution _institution;
        private BlockedPatientListViewModel _viewModel;
        private readonly PatientService _service;

        public UnblockCommand(BlockedPatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new PatientService();
        }

        public override void Execute(object parameter)
        {
            _service.UnblockPatient(_viewModel.SelectedPatientId);
            MessageBox.Show("The patient has ben successfully unblocked.");
            _viewModel.FillPatientList();
        }
    }
}
