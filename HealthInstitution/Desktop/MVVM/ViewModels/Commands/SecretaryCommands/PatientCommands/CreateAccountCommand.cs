using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class CreateAccountCommand : BaseCommand
    {
        private readonly Institution _institution;
        private PatientListViewModel _viewModel;
        private readonly PatientManagementService _service;

        public CreateAccountCommand(PatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new PatientManagementService();
        }

        public override void Execute(object parameter)
        {
            bool validation = ValidateData();
            if (!validation) return;

            Enum.TryParse(_viewModel.NewGender, out Gender gender);
            int newWeight = Int32.Parse(_viewModel.NewWeight);
            int newHeight = Int32.Parse(_viewModel.NewHeight);

            _service.CreatePatient(_viewModel.NewName, _viewModel.NewSurname, _viewModel.NewEmail, _viewModel.NewPassword, gender,
                newHeight, newWeight);
            _viewModel.FillPatientList();

            MessageBox.Show("Successfully created patient account!");
            _viewModel.DialogOpen = false;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.NewName) || string.IsNullOrWhiteSpace(_viewModel.NewSurname)
                || string.IsNullOrWhiteSpace(_viewModel.NewPassword) || string.IsNullOrWhiteSpace(_viewModel.NewEmail)
                || string.IsNullOrWhiteSpace(_viewModel.NewGender))
            {
                MessageBox.Show("You need to fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double newHeight;
            bool isHeightDouble = Double.TryParse(_viewModel.NewHeight, out newHeight);
            if (!isHeightDouble)
            {
                MessageBox.Show("Height must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            double newWeight;
            bool isWeightDouble = Double.TryParse(_viewModel.NewWeight, out newWeight);
            if (!isWeightDouble)
            {
                MessageBox.Show("Weight must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!ReferencesService.CheckIfEmailIsAvailable(_viewModel.NewEmail))
            {
                MessageBox.Show("Account with this email already exist! Please choose a new one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
