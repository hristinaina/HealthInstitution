using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
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
            _viewModel = patientAppointmentViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            Patient patient = _viewModel.Patient;
            Doctor doctor = _viewModel.NewDoctor;
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewDate, _viewModel.NewTime);

            try
            {
                bool done = Institution.Instance().CreateAppointment(doctor, patient, datetime, nameof(Examination));
                if (done)
                {
                    _viewModel.ShowMessage("Appointment successfully scheduled !");
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
