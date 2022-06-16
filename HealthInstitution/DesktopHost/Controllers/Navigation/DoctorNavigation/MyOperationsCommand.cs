using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class MyOperationsCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        public bool Specialization;

        public MyOperationsCommand(bool specialzation)
        {
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
            Specialization = specialzation;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new DoctorOperationViewModel();
        }
    }
}
