using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class NotificationRepositoryService : INotificationRepositoryService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationRepositoryService()
        {
            _notificationRepository = Institution.Instance().NotificationRepository;
        }

        public Notification CreateNotification(int patientID, string text)
        {
            return _notificationRepository.CreateNotification(patientID, text);
        }

        public List<Notification> GetNotifications()
        {
            return _notificationRepository.GetNotifications();
        }
    }
}
