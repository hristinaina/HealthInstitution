using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class RescheduleAppointmentCommand : BaseCommand
    {
        private readonly PatientAppointmentViewModel _viewModel;

        public RescheduleAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel)
        {
            _viewModel = patientAppointmentViewModel;
        }

        public override void Execute(object parameter)
        {

            _viewModel.DialogOpen = false;

            Appointment examination = _viewModel.SelectedAppointment.Appointment;
            DateTime datetime = _viewModel.MergeTime(_viewModel.SelectedDate, _viewModel.SelectedTime);

            try
            {
                PatientRescheduleAppointmentService service = new PatientRescheduleAppointmentService(examination);
                bool doneCompletely = service.RescheduleExamination(datetime);
                if (doneCompletely)
                {
                    _viewModel.ShowMessage("Appointment successfully rescheduled !");
                }
                else
                {
                    _viewModel.ShowMessage("Request sent to secretariat !");
                }
            }
            catch (PatientBlockedException e)
            {
                _viewModel.ShowMessage(e.Message, logOut: true);
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
        }
    }
}
