using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class PatientMainPageViewModel : BaseViewModel
    {

        protected Patient patient;

        public PatientMainPageViewModel(Patient patient)
        {
            this.patient = patient;

            // ..............
        }
    }
}
