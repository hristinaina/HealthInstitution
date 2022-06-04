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
using HealthInstitution.MVVM.Models.Services;

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

            try
            {
                DoctorReferralService service = new();
                bool isCreated = service.CreateReferral(-1, _viewModel.Examination.Patient.ID, specialization);
                
                if (isCreated)
                {
                    _viewModel.ShowMessage("Referral successfully created !");
                }
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
        }
    }
}
