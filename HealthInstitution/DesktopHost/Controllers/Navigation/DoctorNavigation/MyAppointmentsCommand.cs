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
    class MyAppointmentsCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;

        public MyAppointmentsCommand()
        {
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new DoctorExaminationViewModel();
        }
    }
}
