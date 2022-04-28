using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class SecretaryMainPageViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Secretary _secretary;
        public ICommand LogOut { get; }

        public SecretaryMainPageViewModel()
        {
            _institution = Institution.Instance();
            _secretary = (Secretary)_institution.CurrentUser;
            LogOut = new LogOutCommand();

            // ..............
        }
    }
}
