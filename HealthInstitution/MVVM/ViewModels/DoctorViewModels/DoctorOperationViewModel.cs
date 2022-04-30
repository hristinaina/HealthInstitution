using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands;


namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorOperationViewModel : BaseViewModel
    {
        private readonly ObservableCollection<OperationViewModel> _operations;

        private Institution _institution;
        private readonly Doctor _doctor;
        public Doctor Doctor { get => _doctor; }
        public DoctorMedicalRecordViewModel MedicalRecordVM { get; }
        public DoctorNavigationViewModel Navigation { get; }
        public IEnumerable<OperationViewModel> Operations => _operations;

        public ICommand ScheduleOperation { get; }
        public ICommand MedicalRecord { get; }
        public ICommand RescheduleOperation { get; }
        public ICommand DeleteOperation { get; }
        public ICommand CreateOperation { get; }
        public ICommand CancelOperation { get; }

        private OperationViewModel _selectedOperation;
        public OperationViewModel SelectedOperation { get => _selectedOperation; }

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

        public Patient SelectedPatient { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }

        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients => _patients;
        private ObservableCollection<Room> _rooms;
        public ObservableCollection<Room> Rooms => _rooms;
        public Patient NewPatient { get; set; }
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
                _selectedOperation = _operations.ElementAt(_selection);
                SelectedPatient = _selectedOperation.Patient;
                OnPropertyChanged(nameof(SelectedPatient));
                SelectedDate = _selectedOperation.Operation.Date.ToString("MM/dd/yyyy HH:mm");
                OnPropertyChanged(nameof(SelectedDate));
                SelectedTime = _selectedOperation.Operation.Date.ToString("MM/dd/yyyy HH:mm");
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        public DoctorOperationViewModel()
        {
            bool isSpecialist = true;

            Doctor doctor = (Doctor)Institution.Instance().CurrentUser;
            if (doctor.Specialization == Specialization.NONE) isSpecialist = false;
            _operations = new ObservableCollection<OperationViewModel>();
            Navigation = new DoctorNavigationViewModel(isSpecialist);

            _institution = Institution.Instance();
            _doctor = (Doctor)_institution.CurrentUser;
            _patients = new ObservableCollection<Patient>();
            _rooms = new ObservableCollection<Room>();

            EnableChanges = false;

            FillOperationsList();
            FillPatientsList();
            FillRoomsList();

            NewDate = DateTime.Now.ToString("MM/dd/yyyy HH:MM");
            NewTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            CreateOperation = new CreateOperationCommand(this);
            RescheduleOperation = new RescheduleOperationCommand(this);
            CancelOperation = new CancelOperationCommand(this);
            MedicalRecord = new OpenMedicalRecordCommand(this);
        }

        public void FillOperationsList()
        {
            _operations.Clear();
            foreach (Operation operation in _doctor.GetScheduleOfOperations(DateTime.Today))
            {
                _operations.Add(new OperationViewModel(operation));
            }
            OnPropertyChanged(nameof(Operations));
        }

        private void FillPatientsList()
        {
            _patients.Clear();
            foreach (Patient patient in _institution.PatientRepository.Patients)
            {
                _patients.Add(patient);
            }
        }

        private void FillRoomsList()
        {
            _rooms.Clear();
            foreach (Room room in _institution.RoomRepository.Rooms)
            {
                _rooms.Add(room);
            }
        }

        /*public DoctorOperationViewModel()
        {
            Navigation = new DoctorNavigationViewModel();
            _operations = new ObservableCollection<OperationViewModel>();

            // test
            Operation operation = new Operation(1, DateTime.Now, false, false, 30);
            Patient patient = new Patient();
            patient.FirstName = "PAcijnet";
            patient.LastName = "Pacijentic";

            Room room = new Room();
            room.Name = "neka sobica";
            operation.Patient = patient;
            operation.Room = room;
            _operations.Add(new OperationViewModel(operation));
            operation.ID = 2;
            _operations.Add(new OperationViewModel(operation));
            operation.ID = 8;
            _operations.Add(new OperationViewModel(operation));
            operation.Date = DateTime.Now.AddDays(8);
            _operations.Add(new OperationViewModel(operation));
            _operations.Add(new OperationViewModel(operation));

            // test
        }*/
    }
}
