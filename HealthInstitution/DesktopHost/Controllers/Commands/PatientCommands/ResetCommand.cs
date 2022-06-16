using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.PatientServices;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class ResetCommand : BaseCommand
    {
        private IDoctorRepositoryService _doctorService;
        private readonly BaseViewModel _viewModel;

        public ResetCommand(BaseViewModel viewModel)
        {
            _doctorService = new DoctorRepositoryService();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_viewModel is PatientRecordViewModel recordViewModel)
            {
                PatientAppointmentsService service = new PatientAppointmentsService(recordViewModel.Patient);
                recordViewModel.FillAppointmentsList(service.GetPastAppointments());
            }
            if (_viewModel is PatientSearchViewModel searchViewModel)
            {
                searchViewModel.FillAllDoctorsList(_doctorService.GetDoctors());
            }
        }
    }
}
