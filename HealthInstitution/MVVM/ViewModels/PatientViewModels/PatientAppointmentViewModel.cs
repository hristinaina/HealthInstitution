using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
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
        private Institution _institution;

        public PatientNavigationViewModel Navigation { get; }
        private readonly ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments => _appointments;



        public PatientAppointmentViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            Navigation = new PatientNavigationViewModel();
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
