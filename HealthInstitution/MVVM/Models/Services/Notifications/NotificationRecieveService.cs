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
    class NotificationRecieveService
    {
        private Patient _patient;
        private Del _method;
        public delegate void Del(string message);

        public NotificationRecieveService(Patient patient, Del method)
        {
            _patient = patient;
            _method = method;
        }

        public async Task ExecuteRealTimeNotifications()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            await scheduler.Start();

            PatientPrescriptionsService prescriptionsService = new PatientPrescriptionsService(_patient);
            List<Prescription> prescriptions = prescriptionsService.GetUpgoingPrescriptions();

            string message;
            foreach (Prescription prescription in prescriptions)
            {
                message = "Please take medicine " + prescription.Medicine.Name + " !";
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

        private string GetSchedulerPattern(Prescription prescription)
        {
            int hoursBetween = 24 / prescription.TimesADay;
            int advance = 24 + (0 - _patient.NotificationsPreference) % 24;
            int skipFromNow = (DateTime.Now.Hour / hoursBetween) * hoursBetween;
            int startAt = (advance + skipFromNow) % 24;
            return "0/10 * " + startAt + "/" + hoursBetween + " ? * * *";

        }

        public void AddMissedNotifications()
        {
            foreach (Notification notification in _patient.Notifications.ToList())
            {
                if ((DateTime.Now - notification.DateTime).Days < 3)
                {
                    _patient.Notifications.Remove(notification);
                }
            }

            PatientPrescriptionsService prescriptionsService = new PatientPrescriptionsService(_patient);
            List<Prescription> prescriptions = prescriptionsService.GetAllPrescriptions();

            foreach (Prescription prescription in prescriptions)
            {
                DateTime lastTime = prescription.LastNotification;
                if ((DateTime.Now - lastTime).Days > 2)
                {
                    lastTime = DateTime.Now - new TimeSpan(2, 0, 0, 0);
                }
                string message = "Please take medicine " + prescription.Medicine.Name + " in " + _patient.NotificationsPreference + "h !";
                int hoursBetween = 24 / prescription.TimesADay;
                DateTime reminderTime = prescriptionsService.GetPrescriptionDate(prescription);
                DateTime endAt = reminderTime + new TimeSpan(prescription.LongitudeInDays, 0, 0, 0);
                reminderTime -= new TimeSpan(_patient.NotificationsPreference, 0, 0);
                while (reminderTime < lastTime)
                {
                    reminderTime += new TimeSpan(hoursBetween, 0, 0);
                }
                while (reminderTime < DateTime.Today && reminderTime < endAt)
                {
                    Notification newNofitication = new Notification(0, message, reminderTime, _patient.ID);
                    _patient.Notifications.Add(newNofitication);
                    prescription.LastNotification = reminderTime;
                    reminderTime += new TimeSpan(hoursBetween, 0, 0);
                }
            }

            _patient.Notifications = _patient.Notifications.OrderByDescending(x => x.DateTime).ToList();
        }



    }


}
