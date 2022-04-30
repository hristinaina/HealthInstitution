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
                Institution.Instance().CreateAppointment(doctor, patient, datetime, nameof(Examination));
            }
            catch (PatientBlockedException) {
                _viewModel.DialogOpen = false;
                _viewModel.ShowMessage("System has blocked your account !", logOut:true);
            }
            _viewModel.FillAppointmentsList();
        }
    }
}
