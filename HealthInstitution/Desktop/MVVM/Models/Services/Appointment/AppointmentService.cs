using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Desktop.MVVM.Models.Services.Appointment
{
    class AppointmentService
    {
        public bool IsDone(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Now) < 0;
        }
    }
}
