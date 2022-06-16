using System;

namespace HealthInstitution.Core.Repository
{
    public interface INotificationRepository
    {
        public Notification FindByID(int id);

        public int GetNewID();

        public Notification CreateNotification(int patientID, string text);

        public Notification CreateNotification(int patientID, string text, DateTime time);
    }
}