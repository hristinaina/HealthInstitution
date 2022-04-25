using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientSearchViewModel : BaseViewModel
    {
        protected Patient patient;
        public ICommand Record { get; }
        public ICommand Appointments { get; }
        public ICommand LogOut { get; }


        public PatientSearchViewModel(Patient patient)
        {
            this.patient = patient;
            LogOut = new LogOutCommand();
            Record = new PatientRecordCommand();
            Appointments = new PatientAppointmentsCommand();

            // ..............
        }

        public PatientSearchViewModel()
        {
            LogOut = new LogOutCommand();
            Record = new PatientRecordCommand();
            Appointments = new PatientAppointmentsCommand();
        }
    }
}
