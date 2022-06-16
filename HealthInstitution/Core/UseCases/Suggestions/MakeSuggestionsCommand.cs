using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Services;
using System;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class MakeSuggestionsCommand : BaseCommand
    {

        private readonly PatientAppointmentViewModel _viewModel;
        private ISuggestAppointment _suggestionService;

        public MakeSuggestionsCommand(PatientAppointmentViewModel patientAppointmentViewModel)
        {
            _viewModel = patientAppointmentViewModel;
            _suggestionService = new SuggestionsService();
        }

        public override void Execute(object parameter)
        {
            Patient patient = _viewModel.Patient;
            SchedulingPriority priority = (SchedulingPriority)Convert.ToInt32(_viewModel.SuggestionPriority);
            Doctor doctor = _viewModel.SuggestionDoctor;
            DateTime deadlineDate = _viewModel.SuggestionDeadlineDate;
            DateTime startTime = _viewModel.SuggestionStartTime;
            DateTime endTime = _viewModel.SuggestionEndTime;
            ExaminationQuery query = new ExaminationQuery(patient, doctor, startTime, endTime, deadlineDate, priority);
            try
            {
                _viewModel.FillSuggestionsList(_suggestionService.MakeSuggestions(query));
            }
            catch (Exception e) {
                _viewModel.ShowMessage(e.Message);
            }
        }
    }
}
