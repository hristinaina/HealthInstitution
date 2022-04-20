using System.Windows;
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
            AppSettings appSettings = new AppSettings("./Data/patient.json", "./Data/doctor.json", "./Data/secretary.json",
                "./Data/admin.json");
            _institution = Institution.Instance(appSettings);
            _institution.loadAll();

            _navigation = NavigationStore.Instance();


            //Patient p1 = new Patient();
            //p1.Email = "nestolol@gmail.com";
            //p1.Password = "pitajKonobara";
            //Institution.Instance().PatientRepository.GetPatients().Add(p1);

            //Doctor p2 = new Doctor();
            //p2.Email = "nestolol2@gmail.com";
            //p2.Password = "pitajKonobara2";
            //Institution.Instance().DoctorRepository.GetDoctors().Add(p2);

            //Patient p3 = new Patient();
            //p3.Email = "nestolol3@gmail.com";
            //p3.Password = "pitajKonobara3";
            //Institution.Instance().PatientRepository.GetPatients().Add(p3);

            Patient p4 = new Patient();
            p4.Email = "a";
            p4.Password = "a";
            Institution.Instance().PatientRepository.GetPatients().Add(p4);


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
