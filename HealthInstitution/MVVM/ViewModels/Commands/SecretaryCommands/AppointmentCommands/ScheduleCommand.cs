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
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands
{
    public class ScheduleCommand : BaseCommand
    {
        private readonly Institution _institution;
        private EmergencyAppointmentViewModel _viewModel;

        private readonly NavigationStore _navigationStore;

        public ScheduleCommand(EmergencyAppointmentViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            if (_viewModel.SelectedAppointment is null)
            {
                MessageBox.Show("No appointments to reschedule. Recommendation: create a non-emergency appointment !");
                _navigationStore.CurrentViewModel = new AppointmentsViewModel();
                return;
            }

            // reschedule selected appoinment
            Appointment appointmentToPostpone = _viewModel.SelectedAppointment.Appointment;
            DateTime oldDate = appointmentToPostpone.Date;
            DateTime newDate = _viewModel.AppointmentsNewDate[appointmentToPostpone];
            appointmentToPostpone.Date = newDate;

            // create new emergency appointment
            Specialization specialization = _viewModel.SelectedSpecialization;
            Patient patient = _viewModel.SelectedPatient;
            int duration = _viewModel.SelectedDuration;
            string type = duration == 15 ? nameof(Examination) : nameof(Operation);
            Doctor doctor = appointmentToPostpone.Doctor.Specialization == specialization ?
                appointmentToPostpone.Doctor : Institution.Instance().DoctorRepository.FindDoctorBySpecialization(specialization);

            Institution.Instance().CreateAppointment(doctor, patient, oldDate, type, duration, false);
            MessageBox.Show("Emergency appointment has been successfully created !");
            _navigationStore.CurrentViewModel = new AppointmentsViewModel();
        }
    }
}