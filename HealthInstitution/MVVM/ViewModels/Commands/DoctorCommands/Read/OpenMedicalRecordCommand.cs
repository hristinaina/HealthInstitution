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
    class OpenMedicalRecordCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        //private readonly DoctorMedicalRecordViewModel _medicalRecord;
        private DoctorExaminationViewModel _examinationViewModel;
        private DoctorOperationViewModel _operationViewModel;
        private bool _isExaminationViewModel;

        public OpenMedicalRecordCommand(DoctorExaminationViewModel viewModel)
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
            _examinationViewModel = viewModel;
            _isExaminationViewModel = true;
        }

        public OpenMedicalRecordCommand(DoctorOperationViewModel viewModel)
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
            _operationViewModel = viewModel;
            _isExaminationViewModel = false;
        }

        public override void Execute(object parameter)
        {
            if (_isExaminationViewModel)
            _navigationStore.CurrentViewModel = 
            new DoctorMedicalRecordViewModel(_examinationViewModel.SelectedExamination.Examination);

            else
            _navigationStore.CurrentViewModel =
            new DoctorMedicalRecordViewModel(_operationViewModel.SelectedOperation.Operation);
        }
    }
}
