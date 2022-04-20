using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class SecretaryMainPageViewModel : BaseViewModel
    {
        protected Secretary secretary;

        public SecretaryMainPageViewModel(Secretary secretary)
        {
            this.secretary = secretary;

            // ..............
        }
    }
}
