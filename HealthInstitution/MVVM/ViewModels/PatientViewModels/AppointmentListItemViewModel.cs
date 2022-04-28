using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class AppointmentListItemViewModel : BaseViewModel
    {
        Appointment _appointment;

        public string Date => _appointment.Date.ToShortDateString();
        public string Time => _appointment.Date.ToShortTimeString();
        public string Doctor => _appointment.Doctor.GetFullName();
        public string Room => _appointment.Doctor.GetName();

        public AppointmentListItemViewModel(Appointment appointment)
        {
            _appointment = appointment;
        }
    }

}
