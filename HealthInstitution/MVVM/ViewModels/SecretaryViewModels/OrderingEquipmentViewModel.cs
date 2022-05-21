using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class OrderingEquipmentViewModel : BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        // private readonly ObservableCollection<BlockedPatientItemViewModel> _patients;
        // public IEnumerable<BlockedPatientItemViewModel> Patients => _patients;
        // private BlockedPatientItemViewModel _selectedPatient;

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

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; };
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
            }
        }

        public OrderingEquipmentViewModel()
        {
            Navigation = new SecretaryNavigationViewModel();
            EnableChanges = false;
        }
    }
}
