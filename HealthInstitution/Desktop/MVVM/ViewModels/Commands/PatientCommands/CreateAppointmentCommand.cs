﻿using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    public class CreateAppointmentCommand : BaseCommand
    {
        private PatientAppointmentViewModel _viewModel;
        private bool _usingSuggestion;

        public CreateAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel, bool usingSuggestion=false)
        {
            _viewModel = patientAppointmentViewModel;
            _usingSuggestion = usingSuggestion;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            Patient patient = _viewModel.Patient;
            Doctor doctor = _viewModel.NewDoctor;
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewDate, _viewModel.NewTime);

            if (_usingSuggestion) {
                doctor = _viewModel.SelectedSuggestion.Doctor;
                datetime = _viewModel.MergeTime(_viewModel.SelectedSuggestion.Date, _viewModel.SelectedSuggestion.Time);
            }

            try
            {
                PatientScheduleAppointmentService service = new PatientScheduleAppointmentService(patient);
                bool done = service.CreateAppointment(doctor, datetime);
                if (done)
                {
                    _viewModel.ShowMessage("Appointment successfully scheduled !");
                }
            }
            catch (PatientBlockedException e)
            {
                _viewModel.ShowMessage(e.Message, logOut: true);
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
            _viewModel.FillAppointmentsList();
        }
    }
}