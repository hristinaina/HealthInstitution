using System;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.MVVM.ViewModels.MainPageViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.Commands
{
    public class LogInCommand : BaseCommand
    {
        private readonly InstitutionController _institution;
        private readonly NavigationStore _navigationStore;
        private readonly LoginViewModel _loginVM;

        public LogInCommand(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            _institution = InstitutionController.Instance();
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            if (string.IsNullOrEmpty(_loginVM.Email) | string.IsNullOrEmpty(_loginVM.Password))
            {
                //TODO prikazati kao MessageBox: Niste popunili sva polja
                return;
            }

            // check which user type it is and redirect to the corresponding main page
            bool foundUser = Login(_loginVM.Email, _loginVM.Password);

            if (!foundUser)
            {
                //TODO: prikazati kao MessageBox: Ne postoji korisnik sa unesenim podacima!
            }
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        private bool Login(string email, string password)
        {
            PatientController userPatient = MVVM.Models.User.FindUser(_institution.PatientRepository.GetPatients(), email, password);
            if (userPatient != null)
            {
                _navigationStore.CurrentViewModel = new PatientMainPageViewModel(userPatient);
                return true;
            }

            DoctorController userDoctor = MVVM.Models.User.FindUser(_institution.DoctorRepository.GetDoctors(), email, password);
            if (userDoctor != null)
            {
                _navigationStore.CurrentViewModel = new DoctorMainPageViewModel(userDoctor);
                return true;
            }

            SecretaryController userSecretary = MVVM.Models.User.FindUser(_institution.SecretaryRepository.GetSecretaries(), email, password);
            if (userSecretary != null)
            {
                _navigationStore.CurrentViewModel = new SecretaryMainPageViewModel(userSecretary);
                return true;
            }

            AdminController userAdmin = MVVM.Models.User.FindUser(_institution.AdminRepository.GetAdministrators(), email, password);
            if (userAdmin != null)
            {
                _navigationStore.CurrentViewModel = new AdminMainPageViewModel(userAdmin);
                return true;
            }

            return false;
        }
    }
}
