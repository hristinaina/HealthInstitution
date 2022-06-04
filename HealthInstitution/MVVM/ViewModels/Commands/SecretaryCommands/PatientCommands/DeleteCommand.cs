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
    public class DeleteCommand : BaseCommand
    {
        private readonly Institution _institution;
        private PatientListViewModel _viewModel;
        private readonly PatientManagementService _service;

        public DeleteCommand(PatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new PatientManagementService();
        }

        public override void Execute(object parameter)
        {
            _service.DeletePatient(_viewModel.SelectedPatientId);
            MessageBox.Show("The patient has been successfully deleted.");
            _viewModel.FillPatientList();
        }
    }
}
