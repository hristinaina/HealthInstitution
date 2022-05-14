using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Exceptions;

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
                bool isCreated = Institution.Instance().CreateAppointment(doctor, patient, datetime, nameof(Examination));
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
