using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class OpenUpdateMedicalRecordCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        private DoctorExaminationViewModel _examinationViewModel;

        public OpenUpdateMedicalRecordCommand(DoctorExaminationViewModel viewModel)
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
            _examinationViewModel = viewModel;

        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel =
            new UpdateMedicalRecordViewModel(_examinationViewModel.SelectedExamination.Examination);
        }
    }
}
