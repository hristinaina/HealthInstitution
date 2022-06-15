using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class CreatePrescriptionCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public CreatePrescriptionCommand(UpdateMedicalRecordViewModel updateMedicalRecordViewModel)
        {
            _viewModel = updateMedicalRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;

            try
            {
                DoctorPrescriptionService service = new();
                Prescription prescription = new Prescription(0, _viewModel.LongitudeInDays, _viewModel.DailyFrequency,
                                                             _viewModel.SelectedDependency, DateTime.Now, _viewModel.SelectedMedicine);
                bool isCreated = service.CreatePrescription(prescription, _viewModel.Examination);
                if (isCreated) _viewModel.ShowMessage("Prescription successfully created !");
                
            } catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
            
        }
    }
}
