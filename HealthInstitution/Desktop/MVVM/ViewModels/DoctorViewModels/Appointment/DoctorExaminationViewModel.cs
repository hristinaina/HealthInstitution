using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorExaminationViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ExaminationItemViewModel> _examinations;

        private Institution _institution;
        private readonly Doctor _doctor;
        public Doctor Doctor { get => _doctor; }

        public DoctorNavigationViewModel Navigation { get; }
        public DoctorMedicalRecordViewModel MedicalRecordVM { get; }

        public IEnumerable<ExaminationItemViewModel> Examinations => _examinations;

        public ICommand ScheduleExaminationCommand { get; }
        public ICommand StartExamination { get; }
        public ICommand MedicalRecord { get; }
        public ICommand RescheduleAppointment { get; }
        public ICommand CancelAppointment { get; }
        public ICommand CreateExamination { get; }
        public ICommand UpdateMedicalRecord { get; }

        private ExaminationItemViewModel _selectedExamination;
        public ExaminationItemViewModel SelectedExamination { get => _selectedExamination; }

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
                _selectedExamination = _examinations.ElementAt(_selection);
                SelectedPatient = _selectedExamination.Patient;
                OnPropertyChanged(nameof(SelectedPatient));
                SelectedDate = _selectedExamination.Examination.Date.ToString("MM/dd/yyyy HH:mm");
                OnPropertyChanged(nameof(SelectedDate));
                SelectedTime = _selectedExamination.Examination.Date.ToString("MM/dd/yyyy HH:mm");
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        public DoctorExaminationViewModel()
        {
            bool isSpecialist = true;
            Doctor doctor = (Doctor)Institution.Instance().CurrentUser;
            if (doctor.Specialization == Specialization.NONE) isSpecialist = false;
            _examinations = new ObservableCollection<ExaminationItemViewModel>();
            Navigation = new DoctorNavigationViewModel(isSpecialist);

            _institution = Institution.Instance();
            _doctor = (Doctor)_institution.CurrentUser;
            _examinations = new ObservableCollection<ExaminationItemViewModel>();
            _patients = new ObservableCollection<Patient>();
            _rooms = new ObservableCollection<Room>();

            EnableChanges = false;

            FillExaminationsList();
            FillPatientsList();
            FillRoomsList();

            NewDate = DateTime.Now.ToString("MM/dd/yyyy HH:MM");
            NewTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            CreateExamination = new CreateAppointmentCommand(this);
            RescheduleAppointment = new RescheduleAppointmentCommand(this);
            CancelAppointment = new CancelExaminationCommand(this);
            MedicalRecord = new OpenMedicalRecordCommand(this);
            UpdateMedicalRecord = new OpenUpdateMedicalRecordCommand(this);
        }

        public void FillExaminationsList()
        {
            DoctorService doctorService = new DoctorService(_doctor);
            _examinations.Clear();
            foreach (Examination examination in doctorService.GetSchedule(DateTime.Today, nameof(Examination)))
            {
                _examinations.Add(new ExaminationItemViewModel(examination));
            }
            OnPropertyChanged(nameof(Examinations));
            ChangeSelectionIndex();
        }

        public void ChangeSelectionIndex()
        {
            if (_examinations.Count != 0)
            {
                Selection = 0;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                OnPropertyChanged(nameof(EnableChanges));
            }
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
    }
}
