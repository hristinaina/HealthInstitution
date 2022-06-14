using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Desktop.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands
{
    class SubmitCommand : BaseCommand
    {
        private PatientSurveyViewModel _surveyViewModel;
        private PatientRecordViewModel _recordViewModel;

        public SubmitCommand(PatientSurveyViewModel viewModel) {
            _surveyViewModel = viewModel;
        }

        public SubmitCommand(PatientRecordViewModel viewModel) {
            _recordViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_recordViewModel != null)
            {
                CreateReviewService service = new CreateReviewService(_recordViewModel.Service, _recordViewModel.Suggestion, _recordViewModel.Comment);
                service.AssignReview((Examination)_recordViewModel.SelectedAppointment);
                _recordViewModel.CanReview = false;
            }
            else {
                CreateReviewService service = new CreateReviewService(_surveyViewModel.Service, _surveyViewModel.Suggestion, _surveyViewModel.Hygiene, _surveyViewModel.Satisfacion, _viewModel.Comment);
                service.AssignReview();
            }
        }
    }
}
