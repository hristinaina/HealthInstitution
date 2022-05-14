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
    class CreateReferralCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public CreateReferralCommand(UpdateMedicalRecordViewModel updateMedicalRecord)
        {
            _viewModel = updateMedicalRecord;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            try
            {
                if (_viewModel.SelectedDoctor == null) throw new EmptyFieldException("Doctor is not selected !");
                Doctor doctor = _viewModel.SelectedDoctor;
                bool isCreated = Institution.Instance().CreateReferral(doctor.ID, _viewModel.Examination.Patient.ID, doctor.Specialization);

                if (isCreated)
                {
                    _viewModel.ShowMessage("Referral successfully created !");
                }

            } catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }

        }
    }
}
