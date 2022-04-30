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


            Patient p1 = new Patient();
            p1.Email = "p";
            p1.Password = "p";
            Institution.Instance().PatientRepository.Patients.Add(p1);

            Secretary s1 = new Secretary();
            s1.Email = "s";
            s1.Password = "s";
            Institution.Instance().SecretaryRepository.Secretaries.Add(s1);

            Admin a1 = new Admin();
            a1.Email = "a";
            a1.Password = "a";
            Institution.Instance().AdminRepository.Administrators.Add(a1);

            Doctor d1 = new Doctor();
            d1.Email = "d";
            d1.Password = "d";

            Institution.Instance().DoctorRepository.Doctors.Add(d1);

            //List<Examination> l = _institution.PatientRepository.FindByID(1).GetExaminations();

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
    }
}
