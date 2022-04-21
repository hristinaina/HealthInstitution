using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class DoctorMainPageViewModel : BaseViewModel
    {

        protected DoctorController doctor;

        public DoctorMainPageViewModel(DoctorController doctor)
        {
            this.doctor = doctor;

            // ..............
        }

    }
}
