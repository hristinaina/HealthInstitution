using HealthInstitution.MVVM.ViewModels.Commands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands;
using HealthInstitution.MVVM.ViewModels.Commands.PatientCommands.Navigation;
using System.Windows.Input;

namespace HealthInstitution.MVVM.Views.PatientViews
{
    public class PatientNavigationViewModel
    {

        public ICommand Record { get; }
        public ICommand Appointments { get; }
        public ICommand AdvancedScheduling { get; }
        public ICommand Search { get; }
        public ICommand Survey { get; }
        public ICommand LogOut { get; }
        public ICommand Notifications { get; }

        public PatientNavigationViewModel()
        {
            LogOut = new LogOutCommand();
            Record = new RecordContentCommand();
            Appointments = new AppointmentsContentCommand();
            Search = new SearchContentCommand();
            Survey = new SurveyContentCommand();
            Notifications = new NotificationsCommand();
        }
    }
}
