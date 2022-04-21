using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class PatientMainPageViewModel : BaseViewModel
    {

        protected PatientController patient;

        public PatientMainPageViewModel(PatientController patient)
        {
            this.patient = patient;

            // ..............
        }
    }
}
