using Quartz;
using System;
using System.Threading.Tasks;
using System.Windows;
using static HealthInstitution.Core.Services.NotificationReceiveService;

namespace HealthInstitution.Core.Services.Notifications
{
    class NotifyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string message = dataMap.GetString("message");
            Del method = (Del)dataMap.Get("method");
            method(message);
        }
    }

}
