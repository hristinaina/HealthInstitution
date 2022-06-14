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
    class SaveAnamnesisCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public SaveAnamnesisCommand(UpdateMedicalRecordViewModel medicalRecordViewModel)
        {
            _viewModel = medicalRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            Examination examination = _viewModel.Examination;
            examination.Anamnesis = _viewModel.Anamnesis;
            try
            {
                ExaminationService service = new ExaminationService();
                bool isAdded = service.AddAnamnesis(examination, _viewModel.Anamnesis);

                if (isAdded) _viewModel.ShowMessage("Anamnesis successfully added !");
            } 
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
        }
    }
}
