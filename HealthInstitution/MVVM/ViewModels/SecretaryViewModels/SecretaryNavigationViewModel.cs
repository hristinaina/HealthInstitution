using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class SecretaryNavigationViewModel
    {
        public ICommand PatientList { get; }
        public ICommand BlockedPatient { get; }
        public ICommand AppointmentRequests { get; }
        public ICommand LogOut { get; }

        public SecretaryNavigationViewModel()
        {
            LogOut = new LogOutCommand();
            PatientList = new PatientListCommand();
            BlockedPatient = new BlockedPatientCommand();
            AppointmentRequests = new AppointmentRequestsCommand();
            
        }
    }
}
