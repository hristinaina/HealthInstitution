using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    class NotificationListItem : BaseViewModel
    {
        readonly Notification _notification;
        public Notification Appointment { get => _notification; }

        public int Id => _notification.ID;
        public string Date => _notification.DateTime.ToString("dd/MM/yyyy HH:mm");
        public string Text => _notification.Text;

        public NotificationListItem(Notification notification)
        {
            _notification = notification;
        }
    }
}