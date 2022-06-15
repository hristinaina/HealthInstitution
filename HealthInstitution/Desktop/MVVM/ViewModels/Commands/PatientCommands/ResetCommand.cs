using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Services.PatientServices;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class ResetCommand : BaseCommand
    {
        private readonly BaseViewModel _viewModel;

        public ResetCommand(BaseViewModel viewModel)
        {
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
                searchViewModel.FillAllDoctorsList(Institution.Instance().DoctorRepository.Doctors);
            }
        }
    }
}
