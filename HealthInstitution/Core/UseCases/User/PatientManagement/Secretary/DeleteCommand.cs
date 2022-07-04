using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class DeleteCommand : BaseCommand
    {
        private readonly Institution _institution;
        private PatientListViewModel _viewModel;
        private readonly PatientManagementService _service;

        public DeleteCommand(PatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new PatientManagementService();
        }

        public override void Execute(object parameter)
        {
            _service.DeletePatient(_viewModel.SelectedPatientId);
            _viewModel.ShowMessage("The patient has been successfully deleted.");
            _viewModel.FillPatientList();
        }
    }
}
