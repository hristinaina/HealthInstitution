using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services
{
    public interface INotifyRealtime
    {
        public Task ExecuteRealTimeNotifications();
        public string GetSchedulerPattern(Prescription prescription);

    }
}
