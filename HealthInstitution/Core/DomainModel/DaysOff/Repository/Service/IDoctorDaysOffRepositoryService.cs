﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IDoctorDaysOffRepositoryService
    {
        public List<DoctorDaysOff> FindByDoctorID(int doctorId);
    }
}
