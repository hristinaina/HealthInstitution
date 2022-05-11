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

            Institution.Instance().CreatePrescription(_viewModel.SelectedMedicine, _viewModel.LongitudeInDays,
                                                      _viewModel.DailyFrequency, _viewModel.SelectedDependency);
        }
    }
}
