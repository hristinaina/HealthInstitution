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
        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors => _doctors;

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
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedAppointment = _appointments.ElementAt(_selection);
                SelectedDoctor = _selectedAppointment.Doctor;
                OnPropertyChanged(nameof(SelectedDoctor));
                SelectedDate = _selectedAppointment.Date;
                OnPropertyChanged(nameof(SelectedDate));
                SelectedTime = _selectedAppointment.Time;
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        private AppointmentListItemViewModel _selectedAppointment;

        public string SelectedDoctor { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }

        public ICommand CreateAppointment { get; set; }
        
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
            
            CreateAppointment = new CreateAppointmentCommand(this);
            // ..............
        }

        public void FillAppointmentsList()
        {
            _appointments.Clear();
            foreach (Appointment appointment in _patient.GetFutureExaminations())
            {
                _appointments.Add(new AppointmentListItemViewModel(appointment));
            }
        }

        private void FillDoctorsList()
        {
            _doctors.Clear();
            foreach (Doctor doctor in _institution.DoctorRepository.GetGeneralPractitioners()) {
                _doctors.Add(doctor);
            }
        }

    }
}
