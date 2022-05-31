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
    public class PatientListViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }
        private readonly ObservableCollection<PatientListItemViewModel> _patients;
        public IEnumerable<PatientListItemViewModel> Patients => _patients;
        private PatientListItemViewModel _selectedPatient;

        private readonly ObservableCollection<IllnessItemViewModel> _illnesses;
        public IEnumerable<IllnessItemViewModel> Illnesses => _illnesses;

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

        public ICommand Block { get; set; }
        public ICommand Delete { get; set; }
        public ICommand CreateAccount { get; set; }
        public ICommand SaveAccount { get; set; }

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; }
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
                Password = Institution.Instance().PatientRepository.FindByID(Convert.ToInt32(_selectedPatient.Id)).Password;
                OnPropertyChanged(nameof(Password));
                GetGender = Institution.Instance().PatientRepository.FindByID(Convert.ToInt32(_selectedPatient.Id)).Gender.ToString();
                OnPropertyChanged(nameof(GetGender));

                FillAllergenList();
                FillIllnessList();
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

        public int SelectedPatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Password { get; set; }
        public string GetGender { get; set; }

        public PatientListViewModel()
        {
            _patients = new ObservableCollection<PatientListItemViewModel>();
            _allergens = new ObservableCollection<AllergenItemViewModel>();
            _allAllergens = new ObservableCollection<AllergenItemViewModel>();
            _gender = new ObservableCollection<string>();
            _illnesses = new ObservableCollection<IllnessItemViewModel>();
            Navigation = new SecretaryNavigationViewModel();
            Block = new BlockCommand(this);
            Delete = new DeleteCommand(this);
            CreateAccount = new CreateAccountCommand(this);
            SaveAccount = new SaveAccountCommand(this);
            EnableChanges = false;

            FillPatientList();
            FillAllAllergenList();
            FillAllergenList();
            FillGenderList();
            FillIllnessList();
        }

        public void FillPatientList()
        {
            _patients.Clear();

            List<Patient> allPatients = Institution.Instance().PatientRepository.Patients;
            foreach (Patient patient in allPatients)
            {
                if (!patient.Blocked && !patient.Deleted)
                {
                    _patients.Add(new PatientListItemViewModel(patient));
                }
            }

            if (_patients.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }

        public string NewName { get; set; }
        public string NewSurname { get; set; }
        public string NewEmail { get; set; }
        public string NewPassword { get; set; }
        public string NewHeight { get; set; }
        public string NewWeight { get; set; }
        public string NewGender { get; set; }

        private readonly ObservableCollection<AllergenItemViewModel> _allergens;
        public IEnumerable<AllergenItemViewModel> Allergens => _allergens;

        private readonly ObservableCollection<AllergenItemViewModel> _allAllergens;
        public IEnumerable<AllergenItemViewModel> AllAllergens => _allAllergens;

        private readonly ObservableCollection<string> _gender;
        public IEnumerable<string> Gender => _gender;


        public void FillAllAllergenList()
        {
            if (_allAllergens != null)
                _allAllergens.Clear();
            List<Allergen> allergens = Institution.Instance().AllergenRepository.Allergens;
            foreach (Allergen allergen in allergens)
            {
                _allAllergens.Add(new AllergenItemViewModel(allergen));
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
                _allergens.Add(new AllergenItemViewModel(allergen));
            }
        }

        public void FillGenderList()
        {

            _gender.Add("MALE");
            _gender.Add("FEMALE");
            _gender.Add("OTHER");
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
