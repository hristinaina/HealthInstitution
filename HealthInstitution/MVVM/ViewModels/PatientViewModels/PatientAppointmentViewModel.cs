﻿using HealthInstitution.MVVM.Models;
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
        private AppointmentListItemViewModel _selectedAppointment;


        private bool _enableChanges;
        private int _selection;
        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }
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

        public string SelectedDoctor { get; set; }
        public string SelectedDate { get; set; }
        public string SelectedTime { get; set; }

        public PatientNavigationViewModel Navigation { get; }
        private readonly ObservableCollection<AppointmentListItemViewModel> _appointments;
        public IEnumerable<AppointmentListItemViewModel> Appointments => _appointments;



        public PatientAppointmentViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            _appointments = new ObservableCollection<AppointmentListItemViewModel>();
            Navigation = new PatientNavigationViewModel();
            EnableChanges = false;
            FillAppointmentsList();
            // ..............
        }

        public void FillAppointmentsList()
        {
            _appointments.Clear();
            // hardcoded
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
            _appointments.Add(new AppointmentListItemViewModel(new Examination(new Doctor("Marko", "Kljajic"), DateTime.Now, new Room("r1"))));
        }
    }
}