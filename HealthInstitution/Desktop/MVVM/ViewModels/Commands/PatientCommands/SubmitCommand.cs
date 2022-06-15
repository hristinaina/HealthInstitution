﻿using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Desktop.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands
{
    class SubmitCommand : BaseCommand
    {
        private readonly PatientSurveyViewModel _surveyViewModel;
        private readonly PatientRecordViewModel _recordViewModel;

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
                CreateReviewService service = new CreateReviewService(_recordViewModel.Service, _recordViewModel.Suggestion, _recordViewModel.Comment);
                service.AssignReview((Examination)_recordViewModel.SelectedAppointment);
                _recordViewModel.CanReview = false;
                _recordViewModel.ShowMessage("Thank you for your feedback !");
                _recordViewModel.DialogOpen = false;
            }
            else
            {
                CreateReviewService service = new CreateReviewService(_surveyViewModel.Service, _surveyViewModel.Suggestion, _surveyViewModel.Hygiene, _surveyViewModel.Satisfaction, _surveyViewModel.Comment);
                service.AssignReview();
                NavigationStore.Instance().CurrentViewModel = new PatientRecordViewModel();
                NavigationStore.Instance().CurrentViewModel.ShowMessage("Thank you for your feedback !");
            }
        }
    }
}
