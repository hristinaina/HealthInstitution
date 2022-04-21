using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class SecretaryMainPageViewModel : BaseViewModel
    {
        protected SecretaryController secretary;

        public SecretaryMainPageViewModel(SecretaryController secretary)
        {
            this.secretary = secretary;

            // ..............
        }
    }
}
