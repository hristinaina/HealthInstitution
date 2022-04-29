using System;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.MVVM.ViewModels.MainPageViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.Commands
{
    public class LogInCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        private readonly LoginViewModel _loginVM;

        public LogInCommand(LoginViewModel loginVM)
        {
            _loginVM = loginVM;
            _institution = Institution.Instance();
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
            Patient userPatient = MVVM.Models.User.FindUser(_institution.PatientRepository.Patients, email, password);

            if (userPatient != null)
            {
                _navigationStore.CurrentViewModel = new PatientMainPageViewModel(userPatient);
                return true;
            }

            Doctor userDoctor = MVVM.Models.User.FindUser(_institution.DoctorRepository.Doctors, email, password);

            if (userDoctor != null)
            {
                _navigationStore.CurrentViewModel = new DoctorMainPageViewModel(userDoctor);
                return true;
            }

            Secretary userSecretary = MVVM.Models.User.FindUser(_institution.SecretaryRepository.Secretaries, email, password);

            if (userSecretary != null)
            {
                _navigationStore.CurrentViewModel = new SecretaryMainPageViewModel(userSecretary);
                return true;
            }

            Admin
                userAdmin = MVVM.Models.User.FindUser(_institution.AdminRepository.Administrators, email, password);

            if (userAdmin != null)
            {
                _navigationStore.CurrentViewModel = new AdminMainPageViewModel(userAdmin);
                return true;
            }

            return false;
        }
    }
}
