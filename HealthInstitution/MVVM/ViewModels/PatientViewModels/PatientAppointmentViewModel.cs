using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
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
using static HealthInstitution.MVVM.Models.Services.NotificationService;

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
        public DateTime NewDate { get; set; }
        public DateTime NewTime { get; set; }
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
                ChangeSelectionParameters();
            }
        }

        private void ChangeSelectionParameters()
        {
            EnableChanges = true;
            OnPropertyChanged(nameof(EnableChanges));
            OnPropertyChanged(nameof(Selection));
            _selectedAppointment = _appointments.ElementAt(_selection);
            SelectedDoctor = _selectedAppointment.Doctor;
            OnPropertyChanged(nameof(SelectedDoctor));
            SelectedDate = _selectedAppointment.Appointment.Date.ToString("MM/dd/yyyy HH:mm");
            OnPropertyChanged(nameof(SelectedDate));
            SelectedTime = _selectedAppointment.Appointment.Date.ToString("MM/dd/yyyy HH:mm");
            OnPropertyChanged(nameof(SelectedTime));
        }

        private AppointmentListItemViewModel _selectedAppointment;
        public AppointmentListItemViewModel SelectedAppointment { get => _selectedAppointment; }

        public Doctor SelectedDoctor { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }

        public ICommand CreateAppointment { get; set; }
        public ICommand RescheduleAppointment { get; set; }
        public ICommand CancelAppointment { get; set; }

        public ICommand Suggestions { get; set; }
        public Doctor SuggestionDoctor { get; set; }
        public DateTime SuggestionDeadlineDate { get; set; }
        public DateTime SuggestionStartTime { get; set; }
        public DateTime SuggestionEndTime { get; set; }
        public bool SuggestionPriority { get; set; }
        private ObservableCollection<AppointmentListItemViewModel> _appointmentSuggestions;
        public IEnumerable<AppointmentListItemViewModel> AppointmentSuggestions { get => _appointmentSuggestions; }
        public AppointmentListItemViewModel SelectedSuggestion { get; set; }
        public ICommand UseSuggestion { get; set; }

        public PatientAppointmentViewModel()
        {
            Navigation = new PatientNavigationViewModel();

            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            _doctors = new ObservableCollection<Doctor>();

            InitializeChangesParameters();

            FillAppointmentsList();
            FillDoctorsList();

            InitializeCommands();
            InitializeSuggestionsParrameters();

            Del delegateMethod = showNotification;
            NotificationService notifications = new NotificationService(_patient, delegateMethod);
            notifications.ExecuteRealTimeNotifications();
        }

        private void InitializeChangesParameters()
        {
            EnableChanges = false;
            NewTime = DateTime.Now;
            NewDate = DateTime.Now;
        }

        private void InitializeCommands()
        {
            CreateAppointment = new CreateAppointmentCommand(this);
            RescheduleAppointment = new RescheduleAppointmentCommand(this);
            CancelAppointment = new CancelAppointmentCommand(this);
            UseSuggestion = new CreateAppointmentCommand(this, usingSuggestion: true);
        }

        private void InitializeSuggestionsParrameters()
        {
            _appointmentSuggestions = new ObservableCollection<AppointmentListItemViewModel>();
            Suggestions = new MakeSuggestionsCommand(this);
            SuggestionDeadlineDate = DateTime.Now;
            SuggestionStartTime = DateTime.Now;
            SuggestionEndTime = DateTime.Now;
            SuggestionPriority = true;
        }

        public void FillSuggestionsList(List<Examination> suggestions)
        {
            _appointmentSuggestions.Clear();
            foreach (Examination examination in suggestions)
            {
                _appointmentSuggestions.Add(new AppointmentListItemViewModel(examination));
            }
            if (_appointmentSuggestions.Count != 0)
            {
                SelectedSuggestion = _appointmentSuggestions[0];
                OnPropertyChanged(nameof(SelectedSuggestion));
            }
            OnPropertyChanged(nameof(AppointmentSuggestions));

        }

        public void FillAppointmentsList()
        {
            _appointments.Clear();
            foreach (Appointment appointment in _patient.GetFutureAppointments())
            {
                _appointments.Add(new AppointmentListItemViewModel(appointment));
            }
            if (_appointments.Count != 0)
            {
                Selection = 0;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                OnPropertyChanged(nameof(EnableChanges));
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
