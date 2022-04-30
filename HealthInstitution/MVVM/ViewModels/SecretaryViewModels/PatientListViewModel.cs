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
                //OnPropertyChanged(nameof(SelectedDoctor));
                //SelectedDate = _selectedPatient.Date;
                //OnPropertyChanged(nameof(SelectedDate));
                //SelectedTime = _selectedPatient.Time;
                //OnPropertyChanged(nameof(SelectedTime));
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
        //public string SelectedDate { get; set; }
        //public string SelectedTime { get; set; }

        public PatientListViewModel()
        {
            _patients = new ObservableCollection<PatientListItemViewModel>();
            _allergens = new ObservableCollection<AllergenViewModel>();
            _gender = new ObservableCollection<string>();
            Navigation = new SecretaryNavigationViewModel();
            Block = new BlockCommand(this);
            Delete = new DeleteCommand(this);
            CreateAccount = new CreateAccountCommand(this);
            EnableChanges = false;
            FillPatientList();
            FillAllergenList();
            FillGenderList();
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
        }

        public string NewName { get; set; }
        public string NewSurname { get; set; }
        public string NewEmail { get; set; }
        public string NewPassword { get; set; }
        public string NewHeight { get; set; }
        public string NewWeight { get; set; }
        public string NewGender { get; set; }

        private readonly ObservableCollection<AllergenViewModel> _allergens;
        public IEnumerable<AllergenViewModel> Allergens => _allergens;

        private readonly ObservableCollection<string> _gender;
        public IEnumerable<string> Gender => _gender;


        public void FillAllergenList()
        {
            if (_allergens != null)
                _allergens.Clear();
            List<Allergen> allergens = Institution.Instance().AllergenRepository.Allergens;
            foreach (Allergen allergen in allergens)
            {
                _allergens.Add(new AllergenViewModel(allergen));
            }
        }

        public void FillGenderList()
        {

            _gender.Add("MALE");
            _gender.Add("FEMALE");
            _gender.Add("OTHER");
        }
    }
}
