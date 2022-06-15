using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class MakeSuggestionsCommand : BaseCommand
    {

        private readonly PatientAppointmentViewModel _viewModel;

        public MakeSuggestionsCommand(PatientAppointmentViewModel patientAppointmentViewModel)
        {
            _viewModel = patientAppointmentViewModel;
        }

        public override void Execute(object parameter)
        {
            Patient patient = _viewModel.Patient;
            SchedulingPriority priority = (SchedulingPriority)Convert.ToInt32(_viewModel.SuggestionPriority);
            Doctor doctor = _viewModel.SuggestionDoctor;
            DateTime deadlineDate = _viewModel.SuggestionDeadlineDate;
            DateTime startTime = _viewModel.SuggestionStartTime;
            DateTime endTime = _viewModel.SuggestionEndTime;
            _viewModel.FillSuggestionsList(SuggestionsService.MakeSuggestions(patient, priority, doctor, deadlineDate, startTime, endTime));
        }
    }
}
