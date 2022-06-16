using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class SaveAccountCommand : BaseCommand
    {
        private IPatientRepositoryService _patientService;
        private readonly Institution _institution;
        private PatientListViewModel _viewModel;
        private readonly PatientManagementService _service;

        public SaveAccountCommand(PatientListViewModel viewModel)
        {
            _patientService = new PatientRepositoryService();
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new PatientManagementService();
        }

        public override void Execute(object parameter)
        {
            bool validation = ValidateData();
            if (!validation) return;

            int id = _viewModel.SelectedPatientId;
            Enum.TryParse(_viewModel.GetGender, out Gender gender);
            int weight = Int32.Parse(_viewModel.Weight);
            int height =  Int32.Parse(_viewModel.Height);

            _service.UpdatePatient(id, _viewModel.FirstName, _viewModel.LastName, _viewModel.Email, _viewModel.Password, gender,
                height, weight);
            _viewModel.FillPatientList();

            MessageBox.Show("Successfully changed patient data!");
            _viewModel.DialogOpen = false;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.FirstName) || string.IsNullOrWhiteSpace(_viewModel.LastName)
                || string.IsNullOrWhiteSpace(_viewModel.Password) || string.IsNullOrWhiteSpace(_viewModel.Email)
                || string.IsNullOrWhiteSpace(_viewModel.GetGender))
            {
                MessageBox.Show("You need to fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double height;
            bool isHeightDouble = Double.TryParse(_viewModel.Height, out height);
            if (!isHeightDouble)
            {
                MessageBox.Show("Height must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double weight;
            bool isWeightDouble = Double.TryParse(_viewModel.Weight, out weight);
            if (!isWeightDouble)
            {
                MessageBox.Show("Weight must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return ValidateEmail();
        }

        private bool ValidateEmail()
        {
            Patient patient = _patientService.FindByID(_viewModel.SelectedPatientId);

            if (!ReferencesService.CheckIfEmailIsAvailable(_viewModel.Email, patient))
            {
                MessageBox.Show("Account with this email already exist! Please choose a new one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
