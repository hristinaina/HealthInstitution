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

        private readonly InstitutionController _institution;
        private readonly NavigationStore _navigation;
        public App()
        {
            // * appSettings kao parametre prima putanje do fajlova gdje su smjesteni podaci za rad Zdravstvene Ustanove 
            AppSettings appSettings = AppSettings.Instance();
            appSettings.AddFilePaths("./Data/patient.json", "./Data/doctor.json", "./Data/secretary.json",
                "./Data/admin.json");
            _institution = InstitutionController.Instance();
            _institution.LoadAll();

            _navigation = NavigationStore.Instance();


            PatientController p1 = new PatientController();
            p1.Email = "p";
            p1.Password = "p";
            InstitutionController.Instance().PatientRepository.GetPatients().Add(p1);

            SecretaryController s1 = new SecretaryController();
            s1.Email = "s";
            s1.Password = "s";
            InstitutionController.Instance().SecretaryRepository.GetSecretaries().Add(s1);

            AdminController a1 = new AdminController();
            a1.Email = "a";
            a1.Password = "a";
            InstitutionController.Instance().AdminRepository.GetAdministrators().Add(a1);

            DoctorController d1 = new DoctorController();
            d1.Email = "d";
            d1.Password = "d";
            InstitutionController.Instance().DoctorRepository.GetDoctors().Add(d1);


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
