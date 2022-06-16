using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using HealthInstitution.Core.Services.Notifications;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthInstitution.Services
{
    internal class NotificationReceiveService : INotify
    {
        private readonly Patient _patient;
        private readonly Del _method;
        private readonly PatientPrescriptionsService _prescriptionsService;

        public delegate void Del(string message);

        public NotificationReceiveService(Patient patient, Del method)
        {
            _patient = patient;
            _method = method;
            _prescriptionsService = new PatientPrescriptionsService(_patient);

        }

        public async Task ExecuteRealTimeNotifications()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            await scheduler.Start();

            List<Prescription> prescriptions = _prescriptionsService.GetUpgoingPrescriptions();

            string message;
            foreach (Prescription prescription in prescriptions)
            {
                message = "Please take medicine " + prescription.Medicine.Name + " in " + _patient.NotificationsPreference + "h !";
                IJobDetail job = JobBuilder.Create<NotifyJob>()
                                .WithIdentity(prescription.ID.ToString(), _patient.ID.ToString())
                                .Build();
                job.JobDataMap["message"] = message;
                job.JobDataMap["method"] = _method;

                //string schedule = GetSchedulerPattern(prescription);
                //schedule = "0/10 * * ? * * *";
                ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity(prescription.ID.ToString(), _patient.ID.ToString())
              .StartNow()
              .WithSimpleSchedule(x => x
              .WithIntervalInSeconds(10)
              .RepeatForever())
              .ForJob(job)
              .Build();

                //.WithCronSchedule(schedule)
                await scheduler.ScheduleJob(job, trigger);
            }
            await scheduler.Shutdown();
        }

        public string GetSchedulerPattern(Prescription prescription)
        {
            int hoursBetween = 24 / prescription.TimesADay;
            int advance = 24 + (0 - _patient.NotificationsPreference) % 24;
            int skipFromNow = (DateTime.Now.Hour / hoursBetween) * hoursBetween;
            int startAt = (advance + skipFromNow) % 24;
            return "0/10 * " + startAt + "/" + hoursBetween + " ? * * *";

        }

        public void AddMissedNotifications()
        {
            RemoveOutdatedNotifications();

            List<Prescription> prescriptions = _prescriptionsService.GetAllPrescriptions();

            foreach (Prescription prescription in prescriptions)
            {
                if (prescription.Medicine is null) continue;

                DateTime lastTime = FindLastNotificationDate(prescription);
                string message = "Please take medicine " + prescription.Medicine.Name + " in " + _patient.NotificationsPreference + "h !";
                int hoursBetween = 24 / prescription.TimesADay;
                DateTime reminderTime = _prescriptionsService.GetPrescriptionDate(prescription);
                DateTime endAt = reminderTime + new TimeSpan(prescription.LongitudeInDays, 0, 0, 0);
                reminderTime = NextNotificationTime(lastTime, hoursBetween, reminderTime);

                while (reminderTime < DateTime.Today && reminderTime < endAt)
                {
                    Notification newNofitication = new Notification(0, message, reminderTime, _patient.ID);
                    SendNotification(prescription, newNofitication);
                    reminderTime += new TimeSpan(hoursBetween, 0, 0);
                }
            }

            _patient.Notifications = _patient.Notifications.OrderByDescending(x => x.DateTime).ToList();
        }

        public void SendNotification(Prescription prescription, Notification notification)
        {
            _patient.Notifications.Add(notification);
            prescription.LastNotification = notification.DateTime;
        }

        public DateTime NextNotificationTime(DateTime lastTime, int hoursBetween, DateTime reminderTime)
        {
            reminderTime -= new TimeSpan(_patient.NotificationsPreference, 0, 0);

            while (reminderTime < lastTime)
            {
                reminderTime += new TimeSpan(hoursBetween, 0, 0);
            }

            return reminderTime;
        }

        public DateTime FindLastNotificationDate(Prescription prescription)
        {
            DateTime lastTime = prescription.LastNotification;
            if ((DateTime.Now - lastTime).Days > 2)
            {
                lastTime = DateTime.Now - new TimeSpan(2, 0, 0, 0);
            }

            return lastTime;
        }

        public void RemoveOutdatedNotifications()
        {
            foreach (Notification notification in _patient.Notifications.ToList())
            {
                if ((DateTime.Now - notification.DateTime).Days > 3)
                {
                    _patient.Notifications.Remove(notification);
                }
            }
        }
    }


}
