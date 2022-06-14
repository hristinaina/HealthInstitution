using HealthInstitution.Commands;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;

namespace HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands
{
    class CheckCommand : BaseCommand
    {
        private PatientSurveyViewModel _surveyViewModel;
        private PatientRecordViewModel _recordViewModel;

        public CheckCommand(PatientSurveyViewModel viewModel)
        {
            _surveyViewModel = viewModel;
        }

        public CheckCommand(PatientRecordViewModel viewModel)
        {
            _recordViewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            string fullParameter = parameter.ToString();
            string type = fullParameter.Substring(0, fullParameter.Length - 1);
            int value = fullParameter[fullParameter.Length - 1] - '0';
            switch (type)
            {
                case "service":
                    if (_recordViewModel != null)
                    {
                        _recordViewModel.Service = value;
                    }
                    else
                    {
                        _surveyViewModel.Service = value;
                    }
                    break;
                case "hygiene":
                    _surveyViewModel.Hygiene = value;
                    break;
                case "satisfaction":
                    _surveyViewModel.Satisfaction = value;
                    break;
                case "suggestion":
                    if (_recordViewModel != null)
                    {
                        _recordViewModel.Suggestion = value;
                    }
                    else
                    {
                        _surveyViewModel.Suggestion = value;
                    }
                    break;
            }
        }
    }
}
