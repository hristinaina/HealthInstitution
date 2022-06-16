using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class AppointmentListItem : BaseViewModel
    {
        readonly Appointment _appointment;
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
            get
            {
                if (_appointment is Examination) return ((Examination)_appointment).Anamnesis;
                return "";
            }
        }

        public AppointmentListItem(Appointment appointment)
        {
            _appointment = appointment;
        }

        public static implicit operator Appointment(AppointmentListItem a) => a._appointment;
        public static explicit operator AppointmentListItem(Appointment a) => new AppointmentListItem(a);

    }

}
