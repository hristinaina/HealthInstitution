using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;

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
        public int Selection
        {
            get => _selection;
            set
            {
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedPatient = _patients.ElementAt(_selection);
                //SelectedDoctor = _selectedPatient.Doctor;
                //OnPropertyChanged(nameof(SelectedDoctor));
                //SelectedDate = _selectedPatient.Date;
                //OnPropertyChanged(nameof(SelectedDate));
                //SelectedTime = _selectedPatient.Time;
                //OnPropertyChanged(nameof(SelectedTime));
            }
        }

        //public string SelectedDoctor { get; set; }
        //public string SelectedDate { get; set; }
        //public string SelectedTime { get; set; }

        public PatientListViewModel()
        {
            _patients = new ObservableCollection<PatientListItemViewModel>();
            Navigation = new SecretaryNavigationViewModel();
            EnableChanges = false;
            FillPatientList();
            //FillAllergenList();
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

        /*private readonly ObservableCollection<Allergen> _allergens;
        public IEnumerable<Allergen> Allergens => _allergens;

        public void FillAllergenList()
        {
            _allergens.Clear();

            List<Allergen> allergens = Institution.Instance().AllergenRepository.Allergens;
            foreach (Allergen allergen in allergens)
            {
                _allergens.Add(allergen);
            }
        }*/
    }
}
