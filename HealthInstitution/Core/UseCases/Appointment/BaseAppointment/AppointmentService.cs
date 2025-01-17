﻿using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Desktop.MVVM.Models.Services.Appointment
{
    public class AppointmentService : IAppointmentService
    {
        public bool IsDone(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Now) < 0;
        }
    }
}
