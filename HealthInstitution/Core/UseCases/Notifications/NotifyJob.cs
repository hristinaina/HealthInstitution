using Quartz;
using System.Threading.Tasks;
using static HealthInstitution.Services.NotificationReceiveService;

namespace HealthInstitution.Core.Services.Notifications
{
    internal class NotifyJob : IJob
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
