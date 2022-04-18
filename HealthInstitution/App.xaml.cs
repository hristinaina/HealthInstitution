using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        public App()
        {
            _institution = new Institution();
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e) {
            _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
    }
    }
}
