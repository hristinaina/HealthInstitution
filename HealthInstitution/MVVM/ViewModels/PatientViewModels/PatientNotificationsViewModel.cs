using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Views.PatientViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    class PatientNotificationsViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Patient _patient;


        private ObservableCollection<NotificationListItemViewModel> _notifications;
        public IEnumerable<NotificationListItemViewModel> Notifications
        {
            get { return _notifications; }
            set { _notifications = new ObservableCollection<NotificationListItemViewModel>(value); }
        }
        public IEnumerable<string> Hours { get; private set; }
        private int _hour;
        public int SelectedHour {
            get  { return _hour - 1; }

            set { _hour = int.Parse(Hours.ElementAt(value)) + 1; OnPropertyChanged(nameof(SelectedHour)); }
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


        public PatientNavigationViewModel Navigation { get; }


        public PatientNotificationsViewModel()
        {
            _institution = Institution.Instance();
            _patient = (Patient)_institution.CurrentUser;
            Navigation = new PatientNavigationViewModel();
            _notifications = new ObservableCollection<NotificationListItemViewModel>();
            Hours =  new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            FillNotificationsList();

            // ..............
        }

        private void FillNotificationsList()
        {
            _notifications.Clear();
            foreach (Notification notification in _patient.Notifications)
            {
                _notifications.Add(new NotificationListItemViewModel(notification));
            }
            OnPropertyChanged(nameof(Notifications));
        }

    }
}
