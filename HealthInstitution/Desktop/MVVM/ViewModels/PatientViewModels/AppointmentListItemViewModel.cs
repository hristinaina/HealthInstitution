using HealthInstitution.Core;
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

        public int Id => _appointment.ID;
        public bool Emergency => _appointment.Emergency;
        public string Date => _appointment.Date.ToString("dd/MM/yyyy");
        public string Time => _appointment.Date.ToString("HH:mm");
        public Doctor Doctor => _appointment.Doctor;
        public Room Room => _appointment.Room;
        public string Specialization => _appointment.Doctor.Specialization.ToString();
        public string Anamnesis
        {
            get {
                if (_appointment is Examination) return ((Examination)_appointment).Anamnesis;
                return "";
            }
        }

        public AppointmentListItemViewModel(Appointment appointment)
        {
            _appointment = appointment;
        }
    }

}
