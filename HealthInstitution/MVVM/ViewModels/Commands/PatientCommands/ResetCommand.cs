using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.Models.Services.PatientServices;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class ResetCommand : BaseCommand
    {
        private BaseViewModel _viewModel;

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
