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
    class CancelOperationCommand : BaseCommand
    {
        private DoctorOperationViewModel _viewModel;

        public CancelOperationCommand(DoctorOperationViewModel doctorOperationViewModel)
        {
            _viewModel = doctorOperationViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            Appointment operation = _viewModel.SelectedOperation.Operation;
            DoctorCancelAppointmentService doctorCancelAppointmentService = new();
            doctorCancelAppointmentService.CancelExamination((Operation)operation);
            _viewModel.FillOperationsList();
        }
    }
}
