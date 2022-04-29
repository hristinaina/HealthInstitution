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
    class RescheduleAppointmentCommand : BaseCommand
    {
        private PatientAppointmentViewModel _viewModel;

        public RescheduleAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel)
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
            Appointment appointment = _viewModel.SelectedAppointment.Appointment;
            DateTime datetime = _viewModel.MergeTime(_viewModel.SelectedDate, _viewModel.SelectedTime);

            Institution.Instance().RescheduleExamination((Examination)appointment, datetime);
            _viewModel.FillAppointmentsList();
        }
    }
}
