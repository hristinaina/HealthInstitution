using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.AppointmentCommands;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class EmergencyAppointmentViewModel : BaseViewModel
    {
        private AppointmentsViewModel _viewModel;
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments => _appointments;
        private AppointmentListItemViewModel _selectedAppointment;

        private readonly EmergencyAppointmentService _service;
        private readonly ExaminationService _examinationService;
        private readonly OperationService _operationService;
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
            _service = new EmergencyAppointmentService();
            _examinationService = new ExaminationService();
            _operationService = new OperationService();

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
            List<Examination> examinations = _examinationService.GetFutureExaminations(SelectedSpecialization, SelectedPatient);
            List<Operation> operations = _operationService.GetFutureOperations(SelectedSpecialization, SelectedPatient);
            AppointmentsNewDate = new();

            foreach (Operation appointment in operations)
            {
                _service.FindNewAppointmentTime(appointment, appointment.Duration, AppointmentsNewDate);
            }
            foreach (Examination appointment in examinations)
            {
                _service.FindNewAppointmentTime(appointment, 15, AppointmentsNewDate);
            }

            List<Appointment> filteredAppointments = (from entry in AppointmentsNewDate select entry.Key).ToList();
            filteredAppointments = filteredAppointments.OrderBy(x => x.Emergency).ToList();

            foreach (Appointment appointment in filteredAppointments)
            {
                if (new ExaminationService().GetDuration(appointment) >= newDuration)
                    _appointments.Add(new AppointmentListItemViewModel(appointment));
                if (_appointments.Count >= 5) break;
            }

            if (_appointments.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }
    }
}