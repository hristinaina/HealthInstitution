﻿using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Services
{
    public interface ICancelOperation
    {
        public bool CancelOperation(Operation operation);

        public bool CancelAppointment(Appointment appointment);
    }
}
