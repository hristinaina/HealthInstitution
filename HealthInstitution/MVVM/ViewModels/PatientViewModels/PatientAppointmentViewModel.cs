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
        public PatientNavigationViewModel Navigation { get; }

        private readonly Patient _patient;
        public Patient Patient { get => _patient; }
        private Institution _institution;

        private readonly ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments => _appointments;

        private bool _dialogOpen;
        public bool DialogOpen
        {
            get => _dialogOpen;
            set
            {
                _dialogOpen = value;
                OnPropertyChanged(nameof(DialogOpen));
            }
        }


        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors => _doctors;
        public Doctor NewDoctor { get; set; }
        public string NewDate { get; set; }
        public string NewTime { get; set; }
        public Room NewRoom { get; set; }

        private bool _enableChanges;
        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }
        private int _selection;
        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; };
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedAppointment = _appointments.ElementAt(_selection);
                SelectedDoctor = _selectedAppointment.Doctor;
                OnPropertyChanged(nameof(SelectedDoctor));
                SelectedDate = _selectedAppointment.Appointment.Date.ToString("MM/dd/yyyy HH:mm");
                OnPropertyChanged(nameof(SelectedDate));
                SelectedTime = _selectedAppointment.Appointment.Date.ToString("MM/dd/yyyy HH:mm");
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        private AppointmentListItemViewModel _selectedAppointment;
        public AppointmentListItemViewModel SelectedAppointment { get => _selectedAppointment; }

        public Doctor SelectedDoctor { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }

        public ICommand CreateAppointment { get; set; }
        public ICommand RescheduleAppointment { get; set; }
        public ICommand CancelAppointment { get; set; }

        public PatientAppointmentViewModel()
        {
            Navigation = new PatientNavigationViewModel();

            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            _doctors = new ObservableCollection<Doctor>();

            EnableChanges = false;

            FillAppointmentsList();
            FillDoctorsList();

            NewDate = DateTime.Now.ToString("MM/dd/yyyy HH:MM");
            NewTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");

            if (_appointments.Count != 0) {
                Selection = 0;
            }
            CreateAppointment = new CreateAppointmentCommand(this);
            RescheduleAppointment = new RescheduleAppointmentCommand(this);
            CancelAppointment = new CancelAppointmentCommand(this);

        }

        public void FillAppointmentsList()
        {
            _appointments.Clear();
            foreach (Appointment appointment in _patient.GetFutureAppointments())
            {
                _appointments.Add(new AppointmentListItemViewModel(appointment));
            }
            OnPropertyChanged(nameof(Appointments));
        }

        private void FillDoctorsList()
        {
            _doctors.Clear();
            foreach (Doctor doctor in _institution.DoctorRepository.GetGeneralPractitioners())
            {
                _doctors.Add(doctor);
            }
            OnPropertyChanged(nameof(Doctors));
        }
    }
}
