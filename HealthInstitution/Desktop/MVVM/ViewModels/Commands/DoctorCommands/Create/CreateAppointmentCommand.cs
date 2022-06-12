using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;

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

            try
            {
                DoctorScheduleAppointmentService scheduleAppointmentService = new DoctorScheduleAppointmentService();
                Examination examination = new Examination(0, datetime, false, false, "", new ExaminationReview());
                examination.Doctor = (Doctor)Institution.Instance().CurrentUser;
                examination.Patient = patient;

                bool isCreated = scheduleAppointmentService.CreateAppointment(examination, datetime);
                if (isCreated)
                {
                    _viewModel.ShowMessage("Examination successfully scheduled !");
                }
            }
            catch (ExistingAllergenException e)
            {
                _viewModel.ShowMessage(e.Message);
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
            _viewModel.FillExaminationsList();

        }

    }
}
