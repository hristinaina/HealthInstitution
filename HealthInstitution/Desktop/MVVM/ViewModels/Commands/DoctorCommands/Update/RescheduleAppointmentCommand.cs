using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class RescheduleAppointmentCommand : BaseCommand
    {

        private DoctorExaminationViewModel _viewModel;

        public RescheduleAppointmentCommand(DoctorExaminationViewModel doctorExaminationViewModel)
        {
            _viewModel = doctorExaminationViewModel;
        }

        public override void Execute(object parameter)
        {

            _viewModel.DialogOpen = false;

            Examination examination = _viewModel.SelectedExamination.Examination;
            DateTime datetime = _viewModel.MergeTime(_viewModel.SelectedDate, _viewModel.SelectedTime);

            try
            {
                DoctorRescheduleAppointmentService rescheduleAppointmentService = new();
                bool isRescheduled = rescheduleAppointmentService.RescheduleExamination((Examination)examination, datetime);
                if (isRescheduled)
                {
                    _viewModel.ShowMessage("Examination successfully rescheduled !");
                }
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
            _viewModel.FillExaminationsList();
        }
    }
}
