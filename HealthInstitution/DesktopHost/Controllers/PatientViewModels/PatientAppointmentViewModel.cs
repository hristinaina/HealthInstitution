﻿using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.PatientServices;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.Views.PatientViews;
using HealthInstitution.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static HealthInstitution.Services.NotificationReceiveService;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class PatientAppointmentViewModel : BaseViewModel
    {
        private IDoctorRepositoryService _doctorService;
        public PatientNavigationViewModel Navigation { get; }

        private readonly Patient _patient;
        public Patient Patient { get => _patient; }
        private readonly Institution _institution;

        private readonly ObservableCollection<AppointmentListItem> _appointments;
        public IEnumerable<AppointmentListItem> Appointments => _appointments;

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


        private readonly ObservableCollection<Doctor> _doctors;
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

        private AppointmentListItem _selectedAppointment;
        public AppointmentListItem SelectedAppointment { get => _selectedAppointment; }

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
        private ObservableCollection<AppointmentListItem> _appointmentSuggestions;
        private readonly INotify _notifyService;

        public IEnumerable<AppointmentListItem> AppointmentSuggestions { get => _appointmentSuggestions; }
        private AppointmentListItem _selectedSuggestion;
        public AppointmentListItem SelectedSuggestion { get => _selectedSuggestion; set { _selectedSuggestion = value; EnableScheduling = true; } }
        public ICommand UseSuggestion { get; set; }

        private bool _enableScheduling;
        public bool EnableScheduling { get => _enableScheduling; set { _enableScheduling = value; OnPropertyChanged(nameof(EnableScheduling)); } }

        public PatientAppointmentViewModel()
        {
            Navigation = new PatientNavigationViewModel();

            _doctorService = new DoctorRepositoryService();
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItem>();
            _doctors = new ObservableCollection<Doctor>();

            InitializeChangesParameters();

            FillAppointmentsList();
            FillDoctorsList();

            InitializeCommands();
            InitializeSuggestionsParrameters();

            Del delegateMethod = showNotification;
            _notifyService = new NotificationReceiveService(_patient, delegateMethod);
            _notifyService.ExecuteRealTimeNotifications();
            _notifyService.AddMissedNotifications();
        }

        public PatientAppointmentViewModel(Doctor doctor) : this()
        {
            NewDoctor = doctor;
            //DialogOpen = true;
        }

        private void InitializeChangesParameters()
        {
            EnableChanges = false;
            NewTime = DateTime.Now;
            NewDate = DateTime.Now;
        }

        private void InitializeCommands()
        {
            CreateAppointment = new ScheduleExaminationCommand(this);
            RescheduleAppointment = new RescheduleExaminationCommand(this);
            CancelAppointment = new CancelExaminationCommand(this);
            UseSuggestion = new ScheduleExaminationCommand(this, usingSuggestion: true);
        }

        private void InitializeSuggestionsParrameters()
        {
            _appointmentSuggestions = new ObservableCollection<AppointmentListItem>();
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
                _appointmentSuggestions.Add(new AppointmentListItem(examination));
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
            PatientAppointmentsService service = new PatientAppointmentsService(_patient);
            foreach (Appointment appointment in service.GetFutureAppointments())
            {
                _appointments.Add(new AppointmentListItem(appointment));
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

        protected void FillDoctorsList(List<Doctor> doctors = null)
        {
            if (doctors is null)
            {
                doctors = _doctorService.GetGeneralPractitioners();
            }
            _doctors.Clear();
            foreach (Doctor doctor in doctors)
            {
                _doctors.Add(doctor);
            }
            OnPropertyChanged(nameof(Doctors));
        }
    }
}
