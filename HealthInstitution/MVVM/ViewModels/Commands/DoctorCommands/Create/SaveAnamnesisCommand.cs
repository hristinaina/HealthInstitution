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
                bool isAdded = Institution.Instance().AddAnamnesis(examination, _viewModel.Anamnesis);

                if (isAdded) _viewModel.ShowMessage("Anamnesis successfully added !");
            } 
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
        }
    }
}
