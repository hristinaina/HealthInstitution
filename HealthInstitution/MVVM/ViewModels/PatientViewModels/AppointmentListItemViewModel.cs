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

        public string Date => _appointment.GetStart().ToShortDateString();
        public string Time => _appointment.GetStart().ToShortTimeString();
        public string Doctor => _appointment.GetDoctor().GetFullName();
        public string Room => _appointment.GetRoom().GetName();

        public AppointmentListItemViewModel(Appointment appointment)
        {
            _appointment = appointment;
        }
    }

}
