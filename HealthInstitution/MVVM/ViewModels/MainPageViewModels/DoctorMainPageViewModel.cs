using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class DoctorMainPageViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Doctor _doctor;
        public ICommand LogOut { get; }

        public DoctorMainPageViewModel()
        {
            _institution = Institution.Instance();
            _doctor = (Doctor)_institution.CurrentUser;
            LogOut = new LogOutCommand();

            // ..............
        }

    }
}
