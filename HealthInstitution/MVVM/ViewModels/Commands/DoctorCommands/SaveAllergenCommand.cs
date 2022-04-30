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
    class SaveAllergenCommand : BaseCommand
    {
        private UpdateMedicalRecordViewModel _viewModel;

        public SaveAllergenCommand(UpdateMedicalRecordViewModel medicalRecordViewModel)
        {
            _viewModel = medicalRecordViewModel;
        }

        public override void Execute(object parameter)
        {
            Examination examination = _viewModel.Examination;
            Patient patient = examination.Patient;
            patient.Record.Allergens.Add(_viewModel.NewAllergen);
            _viewModel.AddAllergen(_viewModel.NewAllergen);
            Institution.Instance().RescheduleExamination(examination, examination.Date);
        }
    }
}
