using System.Windows;
using System;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;
using HealthInstitution.Core.Services;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;

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
            AppConfiguration.SetInstance(FileService.Deserialize<AppConfiguration>("../../../Infrastructure/Database/Data/databasePaths.json")[0]);
            
            _institution = Institution.Instance();

            _navigation = NavigationStore.Instance();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = new LogInViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigation)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _institution.SaveAll();
            base.OnExit(e);
        }
    }
}
