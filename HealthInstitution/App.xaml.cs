using System.Windows;
using System;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;
using System.Collections.Generic;

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
            appSettings.AddFilePaths("../../../Data/patients.json", "../../../Data/doctors.json",
                                     "../../../Data/secretaries.json", "../../../Data/admins.json",
                                     "../../../Data/examinations.json", "../../../Data/operations.json",
                                     "../../../Data/examinationsReferences.json", "../../../Data/operationsReferences.json",
                                     "../../../Data/equipment.json", "../../../Data/rooms.json", "../../../Data/equipmentInRooms.json",
                                     "../../../Data/medicine.json",
                                     "../../../Data/daysOff.json", "../../../Data/perscriptions.json", "../../../Data/refferal.json");

            _institution = Institution.Instance();

            _navigation = NavigationStore.Instance();


            Patient p1 = new Patient();
            p1.Email = "p";
            p1.Password = "p";
            //Institution.Instance().PatientRepository.GetPatients().Add(p1);

            Secretary s1 = new Secretary();
            s1.Email = "s";
            s1.Password = "s";
            //Institution.Instance().SecretaryRepository.GetSecretaries().Add(s1);

            Admin a1 = new Admin();
            a1.Email = "a";
            a1.Password = "a";
            //Institution.Instance().AdminRepository.GetAdministrators().Add(a1);

            Doctor d1 = new Doctor();
            d1.Email = "d";
            d1.Password = "d";

            //Institution.Instance().DoctorRepository.GetDoctors().Add(d1);

            //List<Examination> l = _institution.PatientRepository.FindByID(1).GetExaminations();

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
