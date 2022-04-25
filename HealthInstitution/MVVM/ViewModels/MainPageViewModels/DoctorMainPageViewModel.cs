using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class DoctorMainPageViewModel : BaseViewModel
    {

        protected Doctor doctor;
        public ICommand LogOut { get; }

        public DoctorMainPageViewModel(Doctor doctor)
        {
            this.doctor = doctor;
            LogOut = new LogOutCommand();

            // ..............
        }

    }
}
