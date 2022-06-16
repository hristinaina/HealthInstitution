using HealthInstitution.Core;
using System;


namespace HealthInstitution.Services
{
    public interface IScheduleExamination
    {
        public bool CreateExamination(Patient patient, Core.Doctor doctor, DateTime dateTime);
    }
}