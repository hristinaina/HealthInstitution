using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class PatientMainPageViewModel : BaseViewModel
    {

        protected Patient patient;
        public ICommand LogOut { get; }


        public PatientMainPageViewModel(Patient patient)
        {
            this.patient = patient;
            LogOut = new LogOutCommand();

            // ..............
        }

        
    }
}
