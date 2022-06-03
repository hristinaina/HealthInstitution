﻿using HealthInstitution.MVVM.ViewModels;
using Quartz;
using System.Threading.Tasks;
using System.Windows;
using static HealthInstitution.MVVM.Models.Services.NotificationService;

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
