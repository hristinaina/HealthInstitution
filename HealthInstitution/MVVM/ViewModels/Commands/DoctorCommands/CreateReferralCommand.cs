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
            Doctor doctor = _viewModel.SelectedDoctor;

            Institution.Instance().CreateReferral(doctor.ID, _viewModel.Examination.Patient.ID, doctor.Specialization);
        }
    }
}
