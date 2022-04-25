using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class SecretaryMainPageViewModel : BaseViewModel
    {
        protected Secretary secretary;
        public ICommand LogOut { get; }

        public SecretaryMainPageViewModel(Secretary secretary)
        {
            this.secretary = secretary;
            LogOut = new LogOutCommand();

            // ..............
        }
    }
}
