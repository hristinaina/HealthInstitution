using Quartz;
using System;
using System.Threading.Tasks;
using System.Windows;
using static HealthInstitution.MVVM.Models.Services.NotificationRecieveService;

namespace HealthInstitution.MVVM.Models.Services.Notifications
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
