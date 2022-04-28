using System.Windows;
using System;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.Stores;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Services;

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
            AppSettings.SetInstance(FileService.Deserialize<AppSettings>("../../../Data/databasePaths.json")[0]);
            //appSettings.AddFilePaths("../../../Data/Patient/patients.json",
            //                         "../../../Data/Doctor/doctors.json",
            //                         "../../../Data/Doctor/daysOff.json",
            //                         "../../../Data/Secretary/secretaries.json", 
            //                         "../../../Data/Admin/admins.json",
            //                         "../../../Data/Appointment/examinations.json", 
            //                         "../../../Data/Appointment/operations.json",
            //                         "../../../Data/Appointment/perscriptions.json",
            //                         "../../../Data/References/examinationsReferences.json", 
            //                         "../../../Data/References/operationsReferences.json",
            //                         "../../../Data/References/refferal.json",
            //                         "../../../Data/Room/equipment.json", 
            //                         "../../../Data/Room/rooms.json", 
            //                         "../../../Data/Room/equipmentInRooms.json",
            //                         "../../../Data/Medicine/medicine.json");

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

            Institution.Instance().GetDoctors().Add(d1);
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
