using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class OpenMedicalRecordCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        private readonly DoctorMedicalRecordViewModel _medicalRecord;

        public OpenMedicalRecordCommand()
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
            _medicalRecord = new DoctorMedicalRecordViewModel();
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = _medicalRecord;
        }
    }
}
