using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class NotificationRepository
    {
        private readonly string _fileName;
        private List<Notification> _notifications;
        public List<Notification> Notifications { get => _notifications; }
        public NotificationRepository(string patientFileName)
        {
            _fileName = patientFileName;
            _notifications = new List<Notification>();
        }

        public void LoadFromFile()
        {
            _notifications = FileService.Deserialize<Notification>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize(_fileName, _notifications);
        }
        public Notification FindByID(int id)
        {
            foreach (Notification notification in _notifications)
            {
                if (notification.ID == id) return notification;
            }
            return null;
        }

        private bool CheckID(int id)
        {
            foreach (Notification n in _notifications)
            {
                if (n.ID == id) return false;
            }
            return true;
        }

        public int GetNewID()
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
