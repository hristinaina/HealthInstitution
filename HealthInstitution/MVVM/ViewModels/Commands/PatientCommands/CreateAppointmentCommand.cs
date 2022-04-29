using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    public class CreateAppointmentCommand : BaseCommand
    {
        private PatientAppointmentViewModel _viewModel;

        public CreateAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel)
        {
            this._viewModel = patientAppointmentViewModel;
        }

        public override void Execute(object parameter)
        {
            Institution.Instance().CreateAppointment();
            _viewModel.DialogOpen = false;
        }
    }
}
