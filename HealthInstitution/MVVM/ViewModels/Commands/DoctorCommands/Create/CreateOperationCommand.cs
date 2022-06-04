using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class CreateOperationCommand : BaseCommand
    {
        private DoctorOperationViewModel _viewModel;

        public CreateOperationCommand(DoctorOperationViewModel doctorOperationViewModel)
        {
            _viewModel = doctorOperationViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            Doctor doctor = _viewModel.Doctor;
            Patient patient = _viewModel.NewPatient;
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewDate, _viewModel.NewTime);

            try
            {
                DoctorScheduleAppointmentService scheduleAppointmentService = new DoctorScheduleAppointmentService();
                Operation operation = new Operation(0, doctor, patient, datetime, _viewModel.Duration);
                operation.Doctor = (Doctor)Institution.Instance().CurrentUser;
                operation.Patient = _viewModel.SelectedPatient;
                bool isCreated = scheduleAppointmentService.CreateAppointment(operation, datetime);
                
                if (isCreated)
                {
                    _viewModel.ShowMessage("Examination successfully scheduled !");
                }
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }

            _viewModel.FillOperationsList();
        }
    }
}
