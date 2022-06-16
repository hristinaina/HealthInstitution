using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface INotificationRepositoryService
    {
        public Notification FindByID(int id);

        public Notification CreateNotification(int patientID, string text);

        public Notification CreateNotification(int patientID, string text, DateTime time);
    }
}
