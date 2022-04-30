using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models;

namespace HealthInstitution.MVVM.ViewModels.DoctorViewModels
{
    class DoctorMedicalRecordViewModel : BaseViewModel
    {
        public DoctorNavigationViewModel Navigation { get; }

        private ObservableCollection<AllergenViewModel> _allergens;
        public IEnumerable<AllergenViewModel> Allergens => _allergens;
        //private MedicalRecordViewModel _medicalRecord;
        //public MedicalRecordViewModel MedicalRecord { get => _medicalRecord; }
        public Examination _examination;
        public Examination Examination { get => _examination; }

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

        public DoctorMedicalRecordViewModel()
        {
            Navigation = new DoctorNavigationViewModel();
            _allergens = new ObservableCollection<AllergenViewModel>();
            _examination = new Examination(1, DateTime.Now, false, false, "", new ExaminationReview(0.0, ""));
            Allergen allergen = new Allergen(1, "Naziv");
            List<Allergen> allergens = new List<Allergen>();
            allergens.Add(allergen);
            Patient patient = new Patient();
            patient.FirstName = "Ime";
            patient.LastName = "Prezime";
            MedicalRecord mr = new MedicalRecord(180, 80, allergens);
            patient.Record = mr;
            _examination.Patient = patient;
            Name = Examination.Patient.FirstName;
            SetProperties();
            FillAllergensList();
        }

        public void SetProperties()
        {
            Name = Examination.Patient.FirstName + " " + Examination.Patient.LastName;
            Height = Examination.Patient.Record.Height;
            Weight = Examination.Patient.Record.Weight;
        }

        public void FillAllergensList()
        {
            _allergens.Clear();
            foreach (Allergen allergen in _examination.Patient.Record.Allergens)
            {
                _allergens.Add(new AllergenViewModel(allergen));
            }
            OnPropertyChanged(nameof(Allergens)) ;
        }

      
    }
}
