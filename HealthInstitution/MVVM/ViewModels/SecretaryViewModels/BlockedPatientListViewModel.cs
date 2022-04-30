using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class BlockedPatientListViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<BlockedPatientItemViewModel> _patients;
        public IEnumerable<BlockedPatientItemViewModel> Patients => _patients;
        private BlockedPatientItemViewModel _selectedPatient;

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

        public ICommand Unblock { get; set; }

        public int SelectedPatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

        private readonly ObservableCollection<AllergenViewModel> _allergens;
        public IEnumerable<AllergenViewModel> Allergens => _allergens;


        private readonly ObservableCollection<IllnessItemViewModel> _illnesses;
        public IEnumerable<IllnessItemViewModel> Illnesses => _illnesses;

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; };
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedPatient = _patients.ElementAt(_selection);
                SelectedPatientId = Convert.ToInt32(_selectedPatient.Id);
                OnPropertyChanged(nameof(SelectedPatientId));
                FirstName = _selectedPatient.Name;
                OnPropertyChanged(nameof(FirstName));
                LastName = _selectedPatient.Surname;
                OnPropertyChanged(nameof(LastName));
                Email = Institution.Instance().PatientRepository.FindByID(Convert.ToInt32(_selectedPatient.Id)).Email;
                OnPropertyChanged(nameof(Email));
                Height = Institution.Instance().PatientRepository.FindByID(Convert.ToInt32(_selectedPatient.Id)).Record.Height.ToString();
                OnPropertyChanged(nameof(Height));
                Weight = Institution.Instance().PatientRepository.FindByID(Convert.ToInt32(_selectedPatient.Id)).Record.Weight.ToString();
                OnPropertyChanged(nameof(Weight));

                FillAllergenList();
                FillIllnessList();
            }
        }

        public BlockedPatientListViewModel()
        {
            _patients = new ObservableCollection<BlockedPatientItemViewModel>();
            _allergens = new ObservableCollection<AllergenViewModel>();
            _illnesses = new ObservableCollection<IllnessItemViewModel>();
            Navigation = new SecretaryNavigationViewModel();
            EnableChanges = false;
            Unblock = new UnblockCommand(this);
            FillPatientList();
            FillAllergenList();
            FillIllnessList();
        }

        public void FillPatientList()
        {
            _patients.Clear();

            List<Patient> blockedPatients = Institution.Instance().PatientRepository.Patients;
            foreach (Patient patient in blockedPatients)
            {
                if (patient.Blocked && !patient.Deleted)
                {
                    _patients.Add(new BlockedPatientItemViewModel(patient));
                }
            }
        }
        public void FillAllergenList()
        {
            _allergens.Clear();
            int id = 1;
            if (_selectedPatient != null) id = Convert.ToInt32(_selectedPatient.Id);
            Patient patient = Institution.Instance().PatientRepository.FindByID(id);
            foreach (Allergen allergen in patient.Record.Allergens)
            {
                _allergens.Add(new AllergenViewModel(allergen));
            }
        }

        public void FillIllnessList()
        {
            _illnesses.Clear();
            int id = 1;
            if (_selectedPatient != null) id = Convert.ToInt32(_selectedPatient.Id);
            List<string> allIllnesses = Institution.Instance().PatientRepository.FindByID(id).GetHistoryOfIllness();
            foreach (string illness in allIllnesses)
            {
                _illnesses.Add(new IllnessItemViewModel(illness));
            }
        }


    }
}
