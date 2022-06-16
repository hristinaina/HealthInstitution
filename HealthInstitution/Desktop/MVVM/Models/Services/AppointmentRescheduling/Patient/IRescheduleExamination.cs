using HealthInstitution.Core;
using System;

namespace HealthInstitution.Services
{
    public interface IRescheduleExamination
    {
        public bool RescheduleExamination(Examination examination, DateTime dateTime);
    }
}
