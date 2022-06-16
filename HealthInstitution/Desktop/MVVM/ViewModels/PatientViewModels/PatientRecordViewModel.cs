using HealthInstitution.Core;
using HealthInstitution.Core.Services.PatientServices;
using HealthInstitution.Desktop.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
using HealthInstitution.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static HealthInstitution.Services.NotificationReceiveService;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientRecordViewModel : BaseViewModel
    {
        private readonly Institution _institution;
        protected Patient _patient;
        public PatientNavigationViewModel Navigation { get; }
        public Patient Patient => _patient;

        private ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments
        {
            get { return _appointments; }
            set { _appointments = new ObservableCollection<AppointmentListItemViewModel>(value); }
        }

        private string _searchKeyWord;
        public string SearchKeyWord
        {
            get { return _searchKeyWord; }
            set { _searchKeyWord = value; OnPropertyChanged(nameof(SearchKeyWord)); }
        }
        public ICommand Search { get; set; }
        public ICommand Reset { get; set; }


        private AppointmentListItemViewModel _selectedAppointment;

        public bool CanReview { get; set; }

        public AppointmentListItemViewModel SelectedAppointment
        {
            get { return _selectedAppointment; }
            set
            {
                _selectedAppointment = value;
                if ((Appointment)_selectedAppointment is Examination examination && examination.Review == null)
                    CanReview = true;
                OnPropertyChanged(nameof(SelectedAppointment));
                OnPropertyChanged(nameof(CanReview));
            }
        }

        private bool _dialogOpen;

        public bool DialogOpen
        {
            get { return _dialogOpen; }
            set { _dialogOpen = value; OnPropertyChanged(nameof(DialogOpen)); }
        }


        private int _service;
        private int _suggestion;
        private string _comment;
        private readonly INotify _notifyService;

        public int Service { get => _service; set { _service = value; } }
        public int Suggestion { get => _suggestion; set { _suggestion = value; } }
        public string Comment { get => _comment; set { _comment = value; OnPropertyChanged(nameof(Comment)); } }

        public ICommand Check { get; set; }
        public ICommand Submit { get; set; }

        public PatientRecordViewModel()
        {
            Navigation = new PatientNavigationViewModel();

            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            PatientAppointmentsService service = new PatientAppointmentsService(_patient);
            FillAppointmentsList(service.GetPastAppointments());
            InitializeSearchParameters();
            CanReview = false;
            Check = new CheckCommand(this);
            Submit = new SubmitSurveyCommand(this);
            Del delegateMethod = showNotification;
            _notifyService = new NotificationReceiveService(_patient, delegateMethod);
            _notifyService.ExecuteRealTimeNotifications();
            _notifyService.AddMissedNotifications();
        }

        private void InitializeSearchParameters()
        {
            _searchKeyWord = "";
            Search = new SearchCommand(this);
            Reset = new ResetCommand(this);
        }

        public void FillAppointmentsList(List<Appointment> appointments)
        {
            _appointments.Clear();
            foreach (Appointment appointment in appointments)
            {
                _appointments.Add(new AppointmentListItemViewModel(appointment));
            }
            OnPropertyChanged(nameof(Appointments));
        }

    }
}
