using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
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
        private Doctor _doctor;
        private DateTime _newDate;

        public CreateEmergencyAppointmentCommand(AppointmentsViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _navigationStore = NavigationStore.Instance();
            _newDuration = 0;
            _doctor = null;
        }

        public override void Execute(object parameter)
        {
            bool validation = ValidateData();
            if (!validation) return;

            _viewModel.DialogOpen = false;
            try
            {
                bool created = CreateAppointment();
                if (created)
                {
                    MessageBox.Show("Appointment has been successfully created!");
                    Appointment newAppointment = SecretaryService.FindAppointment(_viewModel.SelectedPatient, _doctor, _newDate);
                    newAppointment.Emergency = true;
                    _doctor.Notifications.Add("An emergency appointment with id=" + newAppointment.ID.ToString() + " has been scheduled!");
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

        public bool CreateAppointment()
        {
            Specialization specialization = _viewModel.SelectedSpecialization;
            Patient patient = _viewModel.SelectedPatient;
            DateTime startTime = DateTime.Now.AddMinutes(5);
            DateTime currentTime = DateTime.Now.AddMinutes(5);
            _newDate = currentTime;
            string type = _newDuration == 15 ? nameof(Examination) : nameof(Operation);
            bool done = false;

            for (; currentTime < startTime.AddHours(2); currentTime = currentTime.AddMinutes(15))
            {
                bool specialistException = false;
                foreach (Doctor doctor in Institution.Instance().DoctorRepository.Doctors)
                {
                    if (doctor.Specialization == specialization)
                    {
                        specialistException = true;
                        done = Institution.Instance().CreateAppointment(doctor, patient, currentTime, type, _newDuration, false);
                        if (done)
                        {
                            _newDate = currentTime;
                            _doctor = doctor;
                            return true;
                        }
                    }
                }
                if (!specialistException)
                    throw new Exception("There are currently no doctors with selected specialization that work in this hospital.");

            }
            return done;
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
