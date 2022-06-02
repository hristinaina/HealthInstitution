using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    class PrescriptionNotificationService
    {
        public List<Prescription> _prescriptions;

        public PrescriptionNotificationService(List<Prescription> prescriptions)
        {
            _prescriptions = prescriptions;
        }

        public List<Notification> GetNotifications() {

            List<Notification> notifications = new();
            //foreach (Examination appointment in _examinations) { 

            //}
            return notifications;
        }
    }
}
