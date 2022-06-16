using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands
{
    public class CreateEmergencyAppointmentCommand : BaseCommand
    {
        private readonly Institution _institution;
        private AppointmentsViewModel _viewModel;
        private readonly NavigationStore _navigationStore;
        private int _newDuration;
        private readonly EmergencyAppointmentService _service;

        public CreateEmergencyAppointmentCommand(AppointmentsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _navigationStore = NavigationStore.Instance();
            _newDuration = 0;
            _service = new EmergencyAppointmentService();
        }

        public override void Execute(object parameter)
        {
            bool validation = ValidateData();
            if (!validation) return;

            _service.ChangeDuration(_newDuration);
            _viewModel.DialogOpen = false;

            try
            {
                bool created = _service.CreateEmergencyAppointment(_viewModel.SelectedSpecialization, _viewModel.SelectedPatient);
                if (created)
                {
                    MessageBox.Show("Appointment has been successfully created!");
                    _service.NotifyDoctor();
                    return;
                }
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // if appointment hasn't been created
            MessageBox.Show("Please wait few seconds for the system to find appointments that could be postponed.");
            _navigationStore.CurrentViewModel = new EmergencyAppointmentViewModel(_viewModel);
        }

        private bool ValidateData()
        {
            if (_viewModel.SelectedPatient == null)
            {
                MessageBox.Show("Please select a patient!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            bool isDurationInt = Int32.TryParse(_viewModel.SelectedDuration, out _newDuration);
            if (!isDurationInt || _newDuration < 15)
            {
                MessageBox.Show("Duration must be a number > 15", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
