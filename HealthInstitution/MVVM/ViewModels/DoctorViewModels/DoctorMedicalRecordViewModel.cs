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
        private Examination _examination;
        public Examination Examination { get => _examination; }
        private Operation _operation;
        public Operation Operation { get => _operation; }

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

        public DoctorMedicalRecordViewModel(Examination selectedExamination)
        {
            bool isSpecialist = true;
            Doctor doctor = (Doctor) Institution.Instance().CurrentUser;
            if (doctor.Specialization == Specialization.NONE) isSpecialist = false;
            Navigation = new DoctorNavigationViewModel(isSpecialist);
            _allergens = new ObservableCollection<AllergenViewModel>();
            _examination = selectedExamination;

            SetProperties(true);
            FillAllergensList(true);
        }

        public DoctorMedicalRecordViewModel(Operation selectedOperation)
        {
            bool isSpecialist = true;
            Doctor doctor = (Doctor)Institution.Instance().CurrentUser;
            if (doctor.Specialization == Specialization.NONE) isSpecialist = false;
            Navigation = new DoctorNavigationViewModel(isSpecialist);
            _allergens = new ObservableCollection<AllergenViewModel>();
            _operation = selectedOperation;
            SetProperties(false);
            FillAllergensList(false);

        }

        public void SetProperties(bool isExamination)
        {
            if (isExamination)
            {
                Name = _examination.Patient.FirstName + " " + _examination.Patient.LastName;
                Height = _examination.Patient.Record.Height;
                Weight = _examination.Patient.Record.Weight;
            }
            else
            {
                Name = _operation.Patient.FirstName + " " + _operation.Patient.LastName;
                Height = _operation.Patient.Record.Height;
                Weight = _operation.Patient.Record.Weight;
            }
            
        }

        public void FillAllergensList(bool isExamination)
        {
            _allergens.Clear();
            if (isExamination)
            {
                foreach (Allergen allergen in _examination.Patient.Record.Allergens)
                {
                    _allergens.Add(new AllergenViewModel(allergen));
                }
            }
            else
            {
                foreach (Allergen allergen in _operation.Patient.Record.Allergens)
                {
                    _allergens.Add(new AllergenViewModel(allergen));
                }
            }

            OnPropertyChanged(nameof(Allergens));

        }

      
    }
}
