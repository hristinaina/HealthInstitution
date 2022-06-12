using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class SaveChangesCommand : BaseCommand
    {
        private PatientNotificationsViewModel _viewModel;
        private Patient _patient;

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
