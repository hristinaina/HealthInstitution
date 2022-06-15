using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorNavigationViewModel
    {
        public ICommand MyAppointments { get; }
        public ICommand MyOperations { get; }
        public ICommand PendingMedicine { get; }
        public ICommand DaysOffRequests { get; }
        public ICommand LogOut { get; }
        public bool Specialization { get; }

        public DoctorNavigationViewModel(bool isSpecialist)
        {
            LogOut = new LogOutCommand();
            MyAppointments = new MyAppointmentsCommand();
            MyOperations = new MyOperationsCommand(isSpecialist);
            PendingMedicine = new PendingMedicineCommand();
            DaysOffRequests = new DaysOffRequestsCommand();
            Specialization = isSpecialist;
        }
    }
}
