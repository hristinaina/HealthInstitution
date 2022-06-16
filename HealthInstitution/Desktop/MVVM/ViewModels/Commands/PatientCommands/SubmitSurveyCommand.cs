using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Services;
using HealthInstitution.Stores;

namespace HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands
{
    class SubmitCommand : BaseCommand
    {
        private readonly PatientSurveyViewModel _surveyViewModel;
        private readonly PatientRecordViewModel _recordViewModel;
        private IAssignReview _reviewService;

        public SubmitCommand(PatientSurveyViewModel viewModel)
        {
            _surveyViewModel = viewModel;
        }

        public SubmitCommand(PatientRecordViewModel viewModel)
        {
            _recordViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_recordViewModel != null)
            {
                _reviewService = new CreateReviewService(_recordViewModel.Service, _recordViewModel.Suggestion, _recordViewModel.Comment);
                _reviewService.AssignReview((Examination)_recordViewModel.SelectedAppointment);
                _recordViewModel.CanReview = false;
                _recordViewModel.ShowMessage("Thank you for your feedback !");
                _recordViewModel.DialogOpen = false;
            }
            else
            {
                CreateReviewService _reviewService = new CreateReviewService(_surveyViewModel.Service, _surveyViewModel.Suggestion, _surveyViewModel.Hygiene, _surveyViewModel.Satisfaction, _surveyViewModel.Comment);
                _reviewService.AssignReview();
                NavigationStore.Instance().CurrentViewModel = new PatientRecordViewModel();
                NavigationStore.Instance().CurrentViewModel.ShowMessage("Thank you for your feedback !");
            }
        }
    }
}
