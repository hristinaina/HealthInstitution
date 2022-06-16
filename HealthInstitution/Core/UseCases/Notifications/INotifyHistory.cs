using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services
{
    public interface INotifyHistory
    {
        public void AddMissedNotifications();
        public void SendNotification(Prescription prescription, Notification notification);
        public DateTime NextNotificationTime(DateTime lastTime, int hoursBetween, DateTime reminderTime);
        public DateTime FindLastNotificationDate(Prescription prescription);
        public void RemoveOutdatedNotifications();
    }

}
