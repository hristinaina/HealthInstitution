using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Services;
using System;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    public class CreateAppointmentCommand : BaseCommand
    {
        private readonly PatientAppointmentViewModel _viewModel;
        private readonly bool _usingSuggestion;
        private IScheduleExamination _service;

        public CreateAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel, bool usingSuggestion = false)
        {
            _viewModel = patientAppointmentViewModel;
            _usingSuggestion = usingSuggestion;
            _service = new PatientScheduleExaminationService();
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            Patient patient = _viewModel.Patient;
            Doctor doctor = _viewModel.NewDoctor;
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewDate, _viewModel.NewTime);

            if (_usingSuggestion)
            {
                doctor = _viewModel.SelectedSuggestion.Doctor;
                datetime = _viewModel.MergeTime(_viewModel.SelectedSuggestion.Date, _viewModel.SelectedSuggestion.Time);
            }

            try
            {
                bool done = _service.CreateExamination(patient, doctor, datetime);
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
