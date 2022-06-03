using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services.Notifications;
using HealthInstitution.MVVM.ViewModels;
using Quartz;
using Quartz.Impl;
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
        private Del _method;
        private BaseViewModel _viewModel;
        public delegate void Del(string message);

        public NotificationService(Patient patient, Del method)
        {
            _patient = patient;
            _notifications = _patient.Notifications;
            _method = method;
        }

        public async Task ExecuteRealTimeNotifications()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            await scheduler.Start();

            PatientPrescriptionsService prescriptionsService = new PatientPrescriptionsService(_patient);
            List<Prescription> prescriptions = prescriptionsService.GetUpgoingPrescriptions();

            string message = "";
            foreach (Prescription prescription in prescriptions)
            {
                message = "Take medicine " + prescription.Medicine.Name;
                IJobDetail job = JobBuilder.Create<NotifyJob>()
                                .WithIdentity(prescription.ID.ToString(), _patient.ID.ToString())
                                .Build();
                job.JobDataMap["message"] = message;
                job.JobDataMap["method"] = _method;

                // Trigger the job to run now, and then every 40 seconds
                ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity(prescription.ID.ToString(), _patient.ID.ToString())
                  .StartNow()
                  .WithSimpleSchedule(x => x
                      .WithIntervalInSeconds(40)
                      .RepeatForever())
                  .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
            await scheduler.Shutdown();


        }




    }


}
