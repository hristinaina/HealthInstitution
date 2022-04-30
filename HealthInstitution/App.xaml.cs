using System.Windows;
using System;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.Views.DoctorViews;
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
            AppSettings.SetInstance(FileService.Deserialize<AppSettings>("../../../Data/databasePaths.json")[0]);
            
            _institution = Institution.Instance();

            _navigation = NavigationStore.Instance();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            _navigation.CurrentViewModel = new LogInViewModel();
            //_navigation.CurrentViewModel = new AdminRoomViewModel();
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
