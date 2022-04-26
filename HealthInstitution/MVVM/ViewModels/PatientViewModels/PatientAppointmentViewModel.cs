using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientAppointmentViewModel : BaseViewModel
    {
        private readonly Patient _patient;

        private readonly ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments => _appointments;

        public ICommand Record { get; }
        public ICommand Search { get; }
        public ICommand LogOut { get; }


        public PatientAppointmentViewModel(Patient patient)
        {
            _patient = patient;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            LogOut = new LogOutCommand();
            Record = new PatientRecordCommand();
            Search = new PatientSearchCommand();
            FillAppointmentsList();
            // ..............
        }

        public void FillAppointmentsList()
        {
            _appointments.Clear();
            // hardcoded
            _appointments.Add(new AppointmentListItemViewModel(new Appointment(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Appointment(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Appointment(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Appointment(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Appointment(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Appointment(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
        }
    }
}
