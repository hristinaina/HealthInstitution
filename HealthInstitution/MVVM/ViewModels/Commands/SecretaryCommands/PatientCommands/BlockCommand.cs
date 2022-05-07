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
    public class BlockCommand : BaseCommand
    {
        private readonly Institution _institution;
        private PatientListViewModel _viewModel;

        public BlockCommand(PatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            Patient patient = Institution.Instance().PatientRepository.FindByID(_viewModel.SelectedPatientId);
            SecretaryService.BlockPatient(patient);
            string message = "The patient has ben successfully blocked.";
            MessageBox.Show(message);
            _viewModel.FillPatientList();
        }
    }
}
