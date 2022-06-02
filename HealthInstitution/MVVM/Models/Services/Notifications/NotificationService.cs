using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    class NotificationService
    {
        private Patient _patient;
        private List<Notification> _notifications;

        NotificationService(Patient patient)
        {
            _patient = patient;
            _notifications = _patient.Notifications;
        }   
        

    }


}
