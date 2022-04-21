using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class DoctorMainPageViewModel : BaseViewModel
    {

        protected Doctor doctor;

        public DoctorMainPageViewModel(Doctor doctor)
        {
            this.doctor = doctor;

            // ..............
        }

    }
}
