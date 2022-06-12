using HealthInstitution.Commands;
using System.Windows.Input;

namespace HealthInstitution.MVVM.ViewModels
{
    public class LogInViewModel : BaseViewModel
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

        public LogInViewModel()
        {
            Submit = new LogInCommand(this);
        }
    }
}
