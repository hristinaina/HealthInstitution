﻿using System.Windows;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
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
            AppSettings appSettings = AppSettings.Instance();
            appSettings.AddFilePaths("./Data/patient.json", "./Data/doctor.json", "./Data/secretary.json",
                "./Data/admin.json");
            _institution = Institution.Instance();
            _institution.LoadAll();

            _navigation = NavigationStore.Instance();


            Patient p1 = new Patient();
            p1.Email = "p";
            p1.Password = "p";
            Institution.Instance().PatientRepository.GetPatients().Add(p1);

            Secretary s1 = new Secretary();
            s1.Email = "s";
            s1.Password = "s";
            Institution.Instance().SecretaryRepository.GetSecretaries().Add(s1);

            Admin a1 = new Admin();
            a1.Email = "a";
            a1.Password = "a";
            Institution.Instance().AdminRepository.GetAdministrators().Add(a1);

            Doctor d1 = new Doctor();
            d1.Email = "d";
            d1.Password = "d";
            Institution.Instance().DoctorRepository.GetDoctors().Add(d1);


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
