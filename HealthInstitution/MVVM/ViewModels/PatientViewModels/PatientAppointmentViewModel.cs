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
    public class PatientAppointmentViewModel : BaseViewModel
    {
        private readonly Patient _patient;
        private List<Appointment> _appointments;
        public List<Appointment> Appointments
        {
            get => _appointments;
            set { _appointments = value; OnPropertyChanged(nameof(Appointment)); }
        }
        public ICommand Record { get; }
        public ICommand Search { get; }
        public ICommand LogOut { get; }


        public PatientAppointmentViewModel(Patient patient)
        {
            _patient = patient;
            _appointments = patient.Record.Appointments;
            LogOut = new LogOutCommand();
            Record = new PatientRecordCommand();
            Search = new PatientSearchCommand();

            // ..............
        }
    }
}
