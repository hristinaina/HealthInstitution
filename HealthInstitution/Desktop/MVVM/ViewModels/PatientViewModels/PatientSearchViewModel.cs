using HealthInstitution.Core;
using HealthInstitution.Core;
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
    public class PatientSearchViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Patient _patient;
        public PatientNavigationViewModel Navigation { get; }


        private string _firstNameKeyword;

        public string FirstNameKeyWord
        {
            get { return _firstNameKeyword; }
            set { _firstNameKeyword = value; OnPropertyChanged(nameof(FirstNameKeyWord)); }
        }

        private string _lastNameKeyword;

        public string LastNameKeyWord
        {
            get { return _lastNameKeyword; }
            set { _lastNameKeyword = value; OnPropertyChanged(nameof(LastNameKeyWord)); }
        }

        private readonly ObservableCollection<Specialization> _specializations;
        public IEnumerable<Specialization> Specializations => _specializations;

        private int _selectedSpecialization = -1;

        public int SelectedSpecialization
        {
            get { return _selectedSpecialization; }
            set { _selectedSpecialization = value; OnPropertyChanged(nameof(SelectedSpecialization)); }
        }

        private readonly ObservableCollection<DoctorListItemViewModel> _doctors;
        public IEnumerable<DoctorListItemViewModel> Doctors => _doctors;

        private bool _doctorSelected;

        public bool DoctorSelected
        {
            get { return _doctorSelected; }
            set { _doctorSelected = value; OnPropertyChanged(nameof(DoctorSelected)); }
        }

        private DoctorListItemViewModel _doctor;

        public DoctorListItemViewModel DoctorSelectedValue
        {
            get { return _doctor; }
            set { _doctor = value; DoctorSelected = true;  OnPropertyChanged(nameof(DoctorSelectedValue)); }
        }

        public ICommand Search { get; set; }
        public ICommand Reset { get; set; }
        public ICommand CreateAppointment { get; set; }


        public PatientSearchViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            Navigation = new PatientNavigationViewModel();
            _doctors = new();
            _specializations = new();

            Search = new SearchCommand(this);
            Reset = new ResetCommand(this);
            CreateAppointment = new SetSchedulingView(this);

            FillDoctorsList(Institution.Instance().DoctorRepository.Doctors);
            FillSpecializationList();

        }
        public void FillDoctorsList(List<Doctor> doctors)
        {
            _doctors.Clear();
            foreach (Doctor doctor in doctors)
            {
                _doctors.Add(new DoctorListItemViewModel(doctor));
            }
            if (_doctors.Count != 0)
            {
                DoctorSelected = false;
                SelectedSpecialization = -1;
                FirstNameKeyWord = "";
                LastNameKeyWord = "";
                OnPropertyChanged(nameof(DoctorSelected));
                OnPropertyChanged(nameof(SelectedSpecialization));
                OnPropertyChanged(nameof(FirstNameKeyWord));
                OnPropertyChanged(nameof(LastNameKeyWord));
            }
            OnPropertyChanged(nameof(Doctors));
        }

        public void FillSpecializationList()
        {
            foreach (Specialization specialization in Enum.GetValues(typeof(Specialization)))
            {
                _specializations.Add(specialization);
            }
            OnPropertyChanged(nameof(Specializations));
        }
    }
}
