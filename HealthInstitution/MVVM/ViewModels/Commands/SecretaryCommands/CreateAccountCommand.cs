using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class CreateAccountCommand : BaseCommand
    {
        private readonly Institution _institution;
        private PatientListViewModel _viewModel;

        public CreateAccountCommand(PatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.NewName) || string.IsNullOrWhiteSpace(_viewModel.NewSurname)
                || string.IsNullOrWhiteSpace(_viewModel.NewPassword) || string.IsNullOrWhiteSpace(_viewModel.NewEmail)
                || string.IsNullOrWhiteSpace(_viewModel.NewGender)) 
            {
                MessageBox.Show("You need to fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double newHeight;
            bool isHeightDouble = Double.TryParse(_viewModel.NewHeight, out newHeight);
            if (!isHeightDouble)
            {
                MessageBox.Show("Height must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double newWeight;
            bool isWeightDouble = Double.TryParse(_viewModel.NewWeight, out newWeight);
            if (!isWeightDouble)
            {
                MessageBox.Show("Weight must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int id = Institution.Instance().PatientRepository.GetNewID();
            Enum.TryParse(_viewModel.NewGender, out Gender gender);
            Patient patient = new Patient(id, _viewModel.NewName, _viewModel.NewSurname, _viewModel.NewEmail, _viewModel.NewPassword, gender,
                newHeight, newWeight);
            Institution.Instance().PatientRepository.Patients.Add(patient);

            _viewModel.FillPatientList();

            MessageBox.Show("Successfully created patient account!");

            _viewModel.DialogOpen = false;
        }
    }
}
