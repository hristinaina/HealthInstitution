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
    class UpdateMedicalRecordCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public UpdateMedicalRecordCommand(UpdateMedicalRecordViewModel medicalRecordViewModel)
        {
            _viewModel = medicalRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            Examination examination = _viewModel.Examination;
            Patient patient = examination.Patient;
            patient.Record.Weight = _viewModel.Weight;
            patient.Record.Height = _viewModel.Height;
            examination.Patient = patient;
            examination.Anamnesis = _viewModel.Anamnesis;
            patient.Record.Allergens.Add(_viewModel.NewAllergen);
            Institution.Instance().RescheduleExamination(examination, examination.Date);
        }
    }
}
