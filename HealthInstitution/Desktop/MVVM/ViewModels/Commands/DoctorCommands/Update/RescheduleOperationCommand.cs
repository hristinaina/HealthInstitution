using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class RescheduleOperationCommand : BaseCommand
    {
        private DoctorOperationViewModel _viewModel;

        public RescheduleOperationCommand(DoctorOperationViewModel doctorOperationViewModel)
        {
            _viewModel = doctorOperationViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            try
            {
                Operation operation = _viewModel.SelectedOperation.Operation;
                DateTime datetime = _viewModel.MergeTime(_viewModel.SelectedDate, _viewModel.SelectedTime);
                DoctorRescheduleAppointmentService doctorRescheduleAppointmentService = new();
                bool isRescheduled = doctorRescheduleAppointmentService.RescheduleExamination((Operation)operation, datetime);
                if (isRescheduled) _viewModel.ShowMessage("Operation successfully rescheduled !");
            } catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
     
            _viewModel.FillOperationsList();
        }
    }
}
