﻿using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services.DoctorServices
{
    public interface IUserAvailability
    {
        public Appointment FindInterruptingAppointment(DateTime dateTime, int durationInMin = 15);
        public bool IsAvailable(DateTime dateTime, int durationInMin = 15);



    }
}
