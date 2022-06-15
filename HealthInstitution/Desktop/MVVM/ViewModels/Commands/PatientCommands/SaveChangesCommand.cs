using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SaveChangesCommand : BaseCommand
    {
        private readonly PatientNotificationsViewModel _viewModel;
        private readonly Patient _patient;

        public SaveChangesCommand(PatientNotificationsViewModel patientNotificationsViewModel, Patient patient)
        {
            _viewModel = patientNotificationsViewModel;
            _patient = patient;
        }

        public override void Execute(object parameter)
        {

            _viewModel.DialogOpen = false;
            _patient.NotificationsPreference = _viewModel.SelectedHour;
            _viewModel.TextChanged();
        }
    }
}
