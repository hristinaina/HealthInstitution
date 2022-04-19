using System.Windows;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;

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
            // * appSettings kao parametre prima putanje do fajlova gdje su smjesteni podaci za rad Zdravstvene Ustanove 
            AppSettings appSettings = new AppSettings("./Data/patient.json", "./Data/doctor.json", "./Data/secretary.json",
				"./Data/admin.json" );
            _institution = Institution.Instance(appSettings);
            _institution.loadAll();
            
            _navigation = NavigationStore.Instance();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            _navigation.CurrentViewModel = new LoginViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
