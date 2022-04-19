using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.Stores;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand Submit { get; }

        public LoginViewModel()
        {
            Submit = new LogInCommand();
        }
    }
}
