using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface INotificationRepository : IRepository
    {
        public Notification CreateNotification(int patientID, string text);

        public List<Notification> GetNotifications();
    }
}