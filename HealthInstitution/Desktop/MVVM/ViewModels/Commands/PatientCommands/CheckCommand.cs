using HealthInstitution.Commands;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;

namespace HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands
{
    class CheckCommand : BaseCommand
    {
        readonly PatientSurveyViewModel _viewModel;

        public CheckCommand(PatientSurveyViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            string fullParameter = parameter.ToString();
            string type = fullParameter.Substring(0, fullParameter.Length - 1);
            int value = fullParameter[fullParameter.Length - 1] - '0';
            switch (type)
            {
                case "service":
                    _viewModel.Service = value;
                    break;
                case "hygene":
                    _viewModel.Hygiene = value;
                    break;
                case "satisfaction":
                    _viewModel.Satisfacion = value;
                    break;
                case "suggestion":
                    _viewModel.Suggestion = value;
                    break;
            }
        }
    }
}
