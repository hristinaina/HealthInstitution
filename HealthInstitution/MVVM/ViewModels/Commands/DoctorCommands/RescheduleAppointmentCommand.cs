using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

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

            Institution.Instance().RescheduleExamination((Examination)examination, datetime);
            _viewModel.FillExaminationsList();
        }
    }
}
