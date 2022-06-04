using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class MakeSuggestionsCommand : BaseCommand
    {

        private PatientAppointmentViewModel _viewModel;

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
