using System.ComponentModel;
using HealthInstitution.Stores;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.ViewModels.Commands;

namespace HealthInstitution.MVVM.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _notification = "";

        public string Notification
        {
            get { return _notification; }
            set { _notification = value; OnPropertyChanged(nameof(Notification)); }
        }
        private bool _notificationVisibility = false;

        private double _notificationHeight = 40;
        public double NotificationHeight
        {
            get { return _notificationHeight; }
            set { _notificationHeight = value; OnPropertyChanged(nameof(NotificationHeight)); }
        }

        public bool NotificationVisibility
        {
            get { return _notificationVisibility; }
            set { _notificationVisibility = value; OnPropertyChanged(nameof(NotificationVisibility)); }
        }

        private ICommand _dismissNotification;
        public ICommand DismissNotification
        {
            get
            {
                if (_dismissNotification is null)
                {
                    _dismissNotification = new DismissNotificationCommand(this);
                }
                return _dismissNotification;
            }
        }

        public void showNotification(string message)
        {
            if (Notification.Contains(message)) {
                return;
            }
            Notification += message;
            NotificationHeight += 40;
            NotificationVisibility = true;
        }

        public void hideNotification()
        {
            Notification = "";
            NotificationHeight = 0;
            NotificationVisibility = false;
        }

        private string _message = "";
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        private bool _messageVisibility;

        public bool MessageVisibility
        {
            get { return _messageVisibility; }
            set
            {
                _messageVisibility = value;
                OnPropertyChanged(nameof(MessageVisibility));
            }
        }

        public void ShowMessage(string message, bool logOut = false)
        {
            Message = message;
            MessageVisibility = true;
            Wait(logOut);

        }

        public async void Wait(bool logOut)
        {
            await Task.Delay(3000);
            MessageVisibility = false;
            if (logOut)
            {
                NavigationStore.Instance().CurrentViewModel = new LogInViewModel();

            }
        }

        public DateTime MergeTime(string date, string time)
        {
            string[] dateTokens;
            if (date.Split(" ").Length == 3 || date.Split(" ").Length == 2)
            {
                dateTokens = date.Split(" ")[0].Split("/");
            }
            else
            {
                dateTokens = date.Split("-");
                string tmp = dateTokens[0];
                dateTokens[0] = dateTokens[1];
                dateTokens[1] = tmp;
            }
            string[] timeTokens;
            if (time.Split(" ").Length == 3 || time.Split(" ").Length == 2)
            {
                timeTokens = time.Split(" ")[1].Split(":");
            }
            else
            {
                timeTokens = time.Split(":");
            }
            if (dateTokens.Length == 1)
            {
                dateTokens = date.Split(" ")[0].Split("-");
            }
            int month = int.Parse(dateTokens[0]);
            int day = int.Parse(dateTokens[1]);
            int year = int.Parse(dateTokens[2]);
            int hours = int.Parse(timeTokens[0]);
            int minutes = int.Parse(timeTokens[1]);
            return new DateTime(year, month, day, hours, minutes, 0, DateTimeKind.Local);
        }

        public DateTime ParseDate(string date)
        {
            string[] dateTokens;
            if (date.Contains("."))
            {
                dateTokens = date.Split(".");
            }
            else
            {

                dateTokens = date.Split(" ")[0].Split("/");
                if (dateTokens.Length == 1)
                {
                    dateTokens = date.Split(" ")[0].Split("-");
                }
            }
            int month = int.Parse(dateTokens[0]);
            int day = int.Parse(dateTokens[1]);
            int year = int.Parse(dateTokens[2]);
            return new DateTime(year, month, day);
        }
        public DateTime MergeTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0, DateTimeKind.Local);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
