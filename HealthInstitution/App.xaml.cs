using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;
using System.Windows;

namespace HealthInstitution
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly Institution _institution;
        private readonly NavigationStore _navigation;
        public App()
        {
            _institution = Institution.Instance();
            _navigation = NavigationStore.Instance();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = new LoginViewModel(_institution, _navigation);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
