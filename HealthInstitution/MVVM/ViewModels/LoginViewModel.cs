using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.Stores;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand Submit { get; }

        public LoginViewModel(Institution institution, NavigationStore navigationStore)
        {
            Submit = new LogInCommand(institution, navigationStore);
        }
    }
}
