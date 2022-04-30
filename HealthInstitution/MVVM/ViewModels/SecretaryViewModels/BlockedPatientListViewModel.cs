using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;

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

        public ICommand Unblock { get; set; }
        //public ICommand ShowProfile { get; set; }

        public int SelectedPatientId { get; set; }

        public int Selection
        {
            get => _selection;
            set
            {
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

        public BlockedPatientListViewModel()
        {
            _patients = new ObservableCollection<BlockedPatientItemViewModel>();
            Navigation = new SecretaryNavigationViewModel();
            EnableChanges = false;
            Unblock = new UnblockCommand(this);
            FillPatientList();
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
    }
}
