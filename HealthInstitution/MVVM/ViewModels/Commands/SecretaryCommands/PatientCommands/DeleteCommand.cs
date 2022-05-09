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

        public DeleteCommand(PatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            Patient patient = Institution.Instance().PatientRepository.FindByID(_viewModel.SelectedPatientId);
            SecretaryService.DeletePatient(patient);
            string message = "The patient has been successfully deleted.";
            MessageBox.Show(message);
            _viewModel.FillPatientList();
        }
    }
}
