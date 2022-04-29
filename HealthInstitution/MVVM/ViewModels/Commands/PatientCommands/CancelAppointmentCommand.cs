using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class CancelAppointmentCommand : BaseCommand
    {
        private PatientAppointmentViewModel _viewModel;

        public CancelAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel)
        {
            _viewModel = patientAppointmentViewModel;
        }

        public override void Execute(object parameter)
        {

            _viewModel.DialogOpen = false;

            if (_viewModel.Patient.isTrolling())
            {
                return;
            }
            Appointment examination = _viewModel.SelectedAppointment.Appointment;

            Institution.Instance().CancelExamination((Examination)examination);
            _viewModel.FillAppointmentsList();
        }
    }
}
