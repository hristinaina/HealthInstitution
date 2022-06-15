using HealthInstitution.Core;

namespace HealthInstitution.MVVM.ViewModels.PatientViewModels
{
    public class DoctorListItemViewModel
    {
        readonly Doctor _doctor;
        public int Id => _doctor.ID;
        public Doctor Appointment { get => _doctor; }

        public string FirstName => _doctor.FirstName;
        public string LastName => _doctor.LastName;
        public string Specialization => _doctor.Specialization.ToString();
        public double Rating => _doctor.Rating;

        public DoctorListItemViewModel(Doctor doctor)
        {
            _doctor = doctor;
        }

        public static implicit operator Doctor(DoctorListItemViewModel d) => d._doctor;
        public static explicit operator DoctorListItemViewModel(Doctor d) => new DoctorListItemViewModel(d);

    }
}
