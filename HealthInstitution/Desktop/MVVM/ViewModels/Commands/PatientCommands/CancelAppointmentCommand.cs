using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using System;

namespace HealthInstitution.MVVM.ViewModels.Commands.PatientCommands
{
    class CancelAppointmentCommand : BaseCommand
    {
        private readonly PatientAppointmentViewModel _viewModel;

        public CancelAppointmentCommand(PatientAppointmentViewModel patientAppointmentViewModel)
        {
            _viewModel = patientAppointmentViewModel;
        }

        public override void Execute(object parameter)
        {

            _viewModel.DialogOpen = false;

            Appointment examination = _viewModel.SelectedAppointment.Appointment;
            if (examination is Operation)
            {
                _viewModel.ShowMessage("Cannot cancel operation !");
                return;
            }


            try
            {

                PatientCancelAppointmentService service = new PatientCancelAppointmentService((Examination)examination);
                bool doneCompletely = service.CancelExamination();
                if (doneCompletely)
                {
                    _viewModel.ShowMessage("Appointment successfully canceled !");
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

            _viewModel.FillAppointmentsList();
        }
    }
}
