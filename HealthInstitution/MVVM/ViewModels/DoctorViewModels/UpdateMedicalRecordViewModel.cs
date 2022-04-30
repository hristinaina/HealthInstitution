using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands;
using HealthInstitution.MVVM.Models;


namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class UpdateMedicalRecordViewModel : BaseViewModel
    {
        public DoctorNavigationViewModel Navigation { get; }
        
        public ICommand SaveCommand { get; }
        private ObservableCollection<AllergenViewModel> _allergens;
        public IEnumerable<AllergenViewModel> Allergens => _allergens;
        private ObservableCollection<AllergenViewModel> _newAllergens;
        public IEnumerable<AllergenViewModel> NewAllergens => _newAllergens;

        private Examination _examination;
        public Examination Examination { get => _examination; }

        private Patient _patient;
        public Patient Patient { get => _patient; }
        public Allergen NewAllergen { get; set; }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private double _height;
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private double _weight;
        public double Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private string _anamnesis;
        public string Anamnesis
        {
            get
            {
                return _anamnesis;
            }
            set
            {
                _anamnesis = value;
                OnPropertyChanged(nameof(Anamnesis));
            }
        }

        public UpdateMedicalRecordViewModel(Examination examination)
        {
            Navigation = new DoctorNavigationViewModel();
            _examination = examination;
            _allergens = new ObservableCollection<AllergenViewModel>();
            _newAllergens = new ObservableCollection<AllergenViewModel>();

            SaveCommand = new UpdateMedicalRecordCommand(this);

            SetProperties();
            FillAllergensList();
            FillNewAllergenList();
        }

        public void SetProperties()
        {
            Name = Examination.Patient.FirstName + " " + Examination.Patient.LastName;
            Height = Examination.Patient.Record.Height;
            Weight = Examination.Patient.Record.Weight;
            Anamnesis = Examination.Anamnesis;
        }

        public void FillAllergensList()
        {
            _allergens.Clear();
            foreach (Allergen allergen in _examination.Patient.Record.Allergens)
            {
                _allergens.Add(new AllergenViewModel(allergen));
            }
            OnPropertyChanged(nameof(Allergens));
        }

        public void FillNewAllergenList()
        {
            _newAllergens.Clear();

            foreach(Allergen allergen in Institution.Instance().AllergenRepository.Allergens)
            {
                foreach (Allergen i in _examination.Patient.Record.Allergens)
                {
                    if (i.Id == allergen.Id) continue;
                }

                _newAllergens.Add(new AllergenViewModel(allergen));
            }
        }

    }
}
