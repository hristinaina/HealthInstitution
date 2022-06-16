using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Services;
using System;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class RescheduleAppointmentCommand : BaseCommand
    {
        private readonly PatientAppointmentViewModel _viewModel;
        private IRescheduleExamination _service;

        public RescheduleAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel)
        {
            _viewModel = patientAppointmentViewModel;
            _service = new PatientRescheduleExaminationService();
        }

        public override void Execute(object parameter)
        {

            _viewModel.DialogOpen = false;

            Appointment examination = _viewModel.SelectedAppointment.Appointment;
            if (examination is Operation)
            {
                _viewModel.ShowMessage("Cannot reschedule operation !");
                return;
            }


            DateTime datetime = _viewModel.MergeTime(_viewModel.SelectedDate, _viewModel.SelectedTime);

            try
            {
                bool doneCompletely = _service.RescheduleExamination((Examination)examination, datetime);
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
