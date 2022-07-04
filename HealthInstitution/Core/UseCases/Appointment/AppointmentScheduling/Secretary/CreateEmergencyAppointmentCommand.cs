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
                    _viewModel.ShowMessage("Appointment has been successfully created!");
                    _service.NotifyDoctor();
                    return;
                }
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
                return;
            }

            // if appointment hasn't been created
            _viewModel.ShowMessage("Please wait few seconds for the system to find appointments that could be postponed.");
            _navigationStore.CurrentViewModel = new EmergencyAppointmentViewModel(_viewModel);
        }

        private bool ValidateData()
        {
            if (_viewModel.SelectedPatient == null)
            {
                _viewModel.ShowMessage("Please select a patient!");
                return false;
            }
            bool isDurationInt = Int32.TryParse(_viewModel.SelectedDuration, out _newDuration);
            if (!isDurationInt || _newDuration < 15)
            {
                _viewModel.ShowMessage("Duration must be a number > 15");
                return false;
            }
            return true;
        }
    }
}
