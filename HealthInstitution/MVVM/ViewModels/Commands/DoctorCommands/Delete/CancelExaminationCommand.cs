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
    class CancelExaminationCommand : BaseCommand
    {
        private DoctorExaminationViewModel _viewModel;

        public CancelExaminationCommand(DoctorExaminationViewModel doctorExaminationViewModel)
        {
            _viewModel = doctorExaminationViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            Appointment examination = _viewModel.SelectedExamination.Examination;
            DoctorCancelAppointmentService doctorCancelAppointmentService = new();
            doctorCancelAppointmentService.CancelExamination((Examination)examination);
            _viewModel.FillExaminationsList();
        
        }
    }
}
