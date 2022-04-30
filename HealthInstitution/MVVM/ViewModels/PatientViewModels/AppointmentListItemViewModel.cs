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
        public Appointment Appointment { get => _appointment; }

        public string Date => _appointment.Date.ToString("dd/MM/yyyy");
        public string Time => _appointment.Date.ToString("HH:MM");
        public Doctor Doctor => _appointment.Doctor;
        public Room Room => _appointment.Room;

        public AppointmentListItemViewModel(Appointment appointment)
        {
            _appointment = appointment;
        }
    }

}
