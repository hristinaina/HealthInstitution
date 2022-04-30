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
    class CreateAppointmentCommand : BaseCommand
    {
        private DoctorExaminationViewModel _viewModel;

        public CreateAppointmentCommand(DoctorExaminationViewModel doctorExaminationViewModel)
        {
            _viewModel = doctorExaminationViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            Doctor doctor = _viewModel.Doctor;
            Patient patient = _viewModel.NewPatient;
            DateTime datetime = _viewModel.MergeTime(_viewModel.NewDate, _viewModel.NewTime);

            Institution.Instance().CreateAppointment(doctor, patient, datetime, nameof(Examination));
            _viewModel.FillExaminationsList();
        }

    }
}
