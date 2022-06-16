using HealthInstitution.Core;
using System;


namespace HealthInstitution.Services
{
    public interface IScheduleExamination
    {
        public bool CreateExamination(Patient patient, Doctor doctor, DateTime dateTime);
    }
}