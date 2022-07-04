using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands
{
    public class BlockCommand : BaseCommand
    {
        private readonly Institution _institution;
        private PatientListViewModel _viewModel;
        private readonly PatientManagementService _service;

        public BlockCommand(PatientListViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
            _service = new PatientManagementService();
        }

        public override void Execute(object parameter)
        {
            _service.BlockPatient(_viewModel.SelectedPatientId);
            _viewModel.ShowMessage("The patient has been successfully blocked.");
            _viewModel.FillPatientList();
        }
    }
}
