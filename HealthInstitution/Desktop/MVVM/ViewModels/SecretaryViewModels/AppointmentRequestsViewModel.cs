using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels
{
    public class AppointmentRequestsViewModel : BaseViewModel
    {
        private readonly ExaminationChangeService _service;
        public SecretaryNavigationViewModel Navigation { get; }

        private readonly ObservableCollection<AppointmentChangeViewModel> _requests;
        public IEnumerable<AppointmentChangeViewModel> Requests => _requests;
        private AppointmentChangeViewModel _selectedRequest;

        private bool _enableChanges;
        private int _selection;

        public ICommand Approve { get; }
        public ICommand Reject { get; }

        public bool EnableChanges
        {
            get => _enableChanges;
            set
            {
                _enableChanges = value;
                OnPropertyChanged(nameof(EnableChanges));
            }
        }

        public int SelectedRequestId { get; set; }

        public int Selection
        {
            get => _selection;
            set
            {
                if (value < 0) { return; };
                _selection = value;
                EnableChanges = true;
                OnPropertyChanged(nameof(Selection));
                _selectedRequest = _requests.ElementAt(_selection);
                SelectedRequestId = _selectedRequest.ID;
                OnPropertyChanged(nameof(SelectedRequestId));
            }
        }

        public AppointmentRequestsViewModel()
        {
            _requests = new ObservableCollection<AppointmentChangeViewModel>();
            _service = new ExaminationChangeService();
            Navigation = new SecretaryNavigationViewModel();
            Approve = new ApproveCommand(this);
            Reject = new RejectCommand(this);
            EnableChanges = false;
            FillRequestsList();
        }

        public void FillRequestsList()
        {
            _requests.Clear();

            // automaticaly remove/resolve requests for which secretary was late to do so == outdated requests
            _service.RemoveOutdatedRequests();

            List<ExaminationChange> requests = Institution.Instance().ExaminationChangeRepository.Changes;
            foreach (ExaminationChange request in requests)
            {
                if (!request.Resolved)
                {
                    _requests.Add(new AppointmentChangeViewModel(request));
                }
            }

            if (_requests.Count != 0)
            {
                Selection = 0;
                OnPropertyChanged(nameof(Selection));
            }
        }
    }
}
