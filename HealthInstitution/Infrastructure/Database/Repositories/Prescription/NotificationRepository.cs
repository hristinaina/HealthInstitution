using System;
using System.Collections.Generic;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        private List<Notification> _notifications;
        public List<Notification> Notifications { get => _notifications; }
        public NotificationRepository(string patientFileName)
        {
            _fileName = patientFileName;
            _notifications = new List<Notification>();
        }

        public override void LoadFromFile()
        {
            _notifications = FileService.Deserialize<Notification>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize(_fileName, _notifications);
        }

        public List<Notification> GetNotifications() 
        {
            return _notifications;
        }
        private bool CheckID(int id)
        {
            foreach (Notification n in _notifications)
            {
                if (n.ID == id) return false;
            }
            return true;
        }

        private int GetNewID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

        public Notification CreateNotification(int patientID, string text)
        {
            int id = GetNewID();
            Notification notification = new(id, text, DateTime.Now, patientID);
            _notifications.Add(notification);
            return notification;
        }
    }
}
