using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInstitution.Services
{
    public interface IScheduleExamination
    {
        public bool CreateExamination(Patient patient, Doctor doctor, DateTime dateTime);
    }
}