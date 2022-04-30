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

            Appointment examination = _viewModel.SelectedAppointment.Appointment;


            try
            {
                Institution.Instance().CancelExamination((Examination)examination);
            }
            catch (PatientBlockedException)
            {
                _viewModel.DialogOpen = false;
                _viewModel.ShowMessage("System has blocked your account !", logOut: true);
            }

            _viewModel.FillAppointmentsList();
        }
    }
}
