using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class SaveIllnessCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public SaveIllnessCommand(UpdateMedicalRecordViewModel medicalRecordViewModel)
        {
            _viewModel = medicalRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            Examination examination = _viewModel.Examination;
            Patient patient = examination.Patient;
            patient.Record.HistoryOfIllnesses.Add(_viewModel.NewIllness);
            _viewModel.AddIllness(_viewModel.NewIllness);
            try
            {
                PatientService service = new PatientService();
                service.AddIllness(patient, _viewModel.NewIllness);
            } catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }
            /*Institution.Instance().RescheduleExamination(examination, examination.Date);*/
        }
    }
}
