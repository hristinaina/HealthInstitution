using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SearchAnamnesisCommand : BaseCommand
    {
        private PatientRecordViewModel _viewModel;

        public SearchAnamnesisCommand(PatientRecordViewModel patientRecordViewModel)
        {
            _viewModel = patientRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.FillAppointmentsList(AppointmentService.SearchByAnamnesis(_viewModel.Patient, _viewModel.SearchKeyWord));
        }
    }
}
