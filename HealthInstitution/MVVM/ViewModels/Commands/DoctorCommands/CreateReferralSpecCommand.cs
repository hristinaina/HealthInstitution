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
    class CreateReferralSpecCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public CreateReferralSpecCommand(UpdateMedicalRecordViewModel updateMedicalRecord)
        {
            _viewModel = updateMedicalRecord;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
        
            Specialization specialization = _viewModel.SelectedSpecialization;

            Institution.Instance().CreateReferral(-1, _viewModel.Examination.Patient.ID, specialization);
        }
    }
}
