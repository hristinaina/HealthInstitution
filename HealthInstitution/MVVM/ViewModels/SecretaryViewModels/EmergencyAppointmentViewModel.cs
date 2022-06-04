using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.MVVM.Models.Services.DoctorServices;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class EmergencyAppointmentViewModel : BaseViewModel
    {
        private AppointmentsViewModel _viewModel;
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments => _appointments;

        private AppointmentListItemViewModel _selectedAppointment;
        public AppointmentListItemViewModel SelectedAppointment { get => _selectedAppointment; }

        private int _selection;
        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; }
                _selection = value;
                OnPropertyChanged(nameof(Selection));
                _selectedAppointment = _appointments.ElementAt(_selection);
            }
        }

        public Patient SelectedPatient { get; set; }
        public Specialization SelectedSpecialization { get; set; }
        public int SelectedDuration { get; set; }
        public Dictionary<Appointment, DateTime> AppointmentsNewDate { get; set; }

        public ICommand Cancel { get; set; }
        public ICommand Schedule { get; set; }

        public EmergencyAppointmentViewModel(AppointmentsViewModel viewModel)
        {
            _viewModel = viewModel;
            Navigation = new SecretaryNavigationViewModel();
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();

            SelectedSpecialization = _viewModel.SelectedSpecialization;
            SelectedPatient = _viewModel.SelectedPatient;
            SelectedDuration = Int32.Parse(_viewModel.SelectedDuration);

            Cancel = new GoBackCommand(this);
            Schedule = new ScheduleCommand(this);

            FillAppointmentsList();
        }

        public void FillAppointmentsList()
        {
            _appointments.Clear();
            int newDuration = Int32.Parse(_viewModel.SelectedDuration);
            List<Examination> examinations = Institution.Instance().ExaminationRepository.GetFutureExaminations(SelectedSpecialization, SelectedPatient);
            List<Operation> operations = Institution.Instance().OperationRepository.GetFutureOperations(SelectedSpecialization, SelectedPatient);
            AppointmentsNewDate = new();

            foreach (Operation appointment in operations)
            {
                FindNewAppointmentTime(appointment, appointment.Duration, AppointmentsNewDate);
            }
            foreach (Examination appointment in examinations)
            {
                FindNewAppointmentTime(appointment, 15, AppointmentsNewDate);
            }

            List<Appointment> filteredAppointments = (from entry in AppointmentsNewDate select entry.Key).ToList();
            filteredAppointments = filteredAppointments.OrderBy(x => x.Emergency).ToList();

            foreach (Appointment appointment in filteredAppointments)
            {
                if (GetDuration(appointment) >= newDuration)
                    _appointments.Add(new AppointmentListItemViewModel(appointment));
                if (_appointments.Count >= 5) break;
            }

            if (_appointments.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }

        public static void FindNewAppointmentTime(Appointment appointment, int duration, Dictionary<Appointment, DateTime> appointments)
        {
            //if (appointment.Emergency) return;
            DateTime startTime = appointment.Date;

            while (true)
            {
                startTime = startTime.AddMinutes(15);
                try
                {
                    bool done = CheckNewAppointmentTime(appointment.Doctor, appointment.Patient, startTime, duration, false);
                    if (done)
                    {
                        appointments.Add(appointment, startTime);
                        break;
                    }
                }
                catch (Exception e) { }
            }
        }

        private static bool CheckNewAppointmentTime(Doctor doctor, Patient patient, DateTime dateTime, int duration, bool validation = false)
        {
            DoctorService doctorService = new DoctorService(doctor);
            if (!doctorService.IsAvailable(dateTime, duration))
            {
                return false;
            }
            if (!patient.IsAvailable(dateTime, duration))
            {
                return false;
            }
            Institution.Instance().ValidateAppointmentData(patient, doctor, dateTime, validation);
            return true;
        }

        public static int GetDuration(Appointment appointment)
        {
            int duration = 15;
            if (appointment.GetType() == typeof(Operation))
            {
                Operation o = (Operation)appointment;
                duration = o.Duration;
            }
            return duration;
        }
    }
}