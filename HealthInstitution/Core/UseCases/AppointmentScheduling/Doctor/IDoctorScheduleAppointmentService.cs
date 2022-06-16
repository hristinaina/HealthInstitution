using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    interface IDoctorScheduleAppointmentService
    {
        public bool CreateAppointment(Appointment appointment, DateTime dateTime, bool validation = true);
        public bool CreateExamination(Appointment appointment, DateTime dateTime);
        public bool CreateOperation(Appointment appointment, DateTime dateTime);

    }
}
