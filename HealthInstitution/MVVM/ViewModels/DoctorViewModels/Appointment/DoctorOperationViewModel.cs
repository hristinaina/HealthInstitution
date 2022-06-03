﻿using System;
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
        private readonly ObservableCollection<OperationItemViewModel> _operations;

        private Institution _institution;
        private readonly Doctor _doctor;
        public Doctor Doctor { get => _doctor; }
        public DoctorMedicalRecordViewModel MedicalRecordVM { get; }
        public DoctorNavigationViewModel Navigation { get; }
        public IEnumerable<OperationItemViewModel> Operations => _operations;

        public ICommand ScheduleOperation { get; }
        public ICommand MedicalRecord { get; }
        public ICommand RescheduleOperation { get; }
        public ICommand DeleteOperation { get; }
        public ICommand CreateOperation { get; }
        public ICommand CancelOperation { get; }

        private OperationItemViewModel _selectedOperation;
        public OperationItemViewModel SelectedOperation { get => _selectedOperation; }

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
        public int Duration { get; set; }
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
            _operations = new ObservableCollection<OperationItemViewModel>();
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
            Duration = 15;
            CreateOperation = new CreateOperationCommand(this);
            RescheduleOperation = new RescheduleOperationCommand(this);
            CancelOperation = new CancelOperationCommand(this);
            MedicalRecord = new OpenMedicalRecordCommand(this);
        }

        public void FillOperationsList()
        {
            _operations.Clear();
            foreach (Operation operation in _doctor.GetSchedule(DateTime.Today, nameof(Operation)))
            {
                _operations.Add(new OperationItemViewModel(operation));
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
    }
}