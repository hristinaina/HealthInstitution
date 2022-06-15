using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class DoctorDaysOffViewModel: BaseViewModel
    {
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<DaysOffItemViewModel> _requests;
        public IEnumerable<DaysOffItemViewModel> Requests => _requests;
        private DaysOffItemViewModel _selectedRequest { get; set; }
        public DaysOffItemViewModel SelectedRequest { get => _selectedRequest; set => _selectedRequest = value; }

        public ICommand AcceptRequestCommand { get; set; }
        public ICommand RejectRequestCommand { get; set; }

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

        private bool _enableChanges;
        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }

        private int _selection;
        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; }
                _selection = value;
                OnPropertyChanged(nameof(Selection));
                _selectedRequest = _requests.ElementAt(_selection);
                EnableChanges = true;
            }
        }

        public DoctorDaysOffViewModel()
        {
            Navigation = new SecretaryNavigationViewModel();
            EnableChanges = false;

            _requests = new ObservableCollection<DaysOffItemViewModel>();
            AcceptRequestCommand = new AcceptRequestCommand(this);
            RejectRequestCommand = new RejectRequestCommand(this);

            FillRequestsList();
        }

        public void FillRequestsList()
        {
            _requests.Clear();
            foreach (DayOff df in Institution.Instance().DayOffRepository.DaysOff)
            {
                if (df.State == State.ON_HOLD) _requests.Add(new DaysOffItemViewModel(df));
            }

            if (_requests.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }
    }
}
