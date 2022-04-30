using System;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.MVVM.ViewModels.MainPageViewModels;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;
using HealthInstitution.Stores;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.Commands
{
    public class LogInCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        private readonly LogInViewModel _loginVM;

        public LogInCommand(LogInViewModel loginVM)
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
            _institution.CurrentUser = User.FindUser(_institution.PatientRepository.Patients, email, password);
            if (_institution.CurrentUser != null)
            {
                _navigationStore.CurrentViewModel = new PatientAppointmentViewModel();
                return true;
            }

            _institution.CurrentUser = User.FindUser(_institution.DoctorRepository.Doctors, email, password);
            if (_institution.CurrentUser != null)
            {
                _navigationStore.CurrentViewModel = new DoctorExaminationViewModel();
                return true;
            }

            _institution.CurrentUser = User.FindUser(_institution.SecretaryRepository.Secretaries, email, password);
            if (_institution.CurrentUser != null)
            {
                _navigationStore.CurrentViewModel = new PatientListViewModel();
                return true;
            }

            _institution.CurrentUser = User.FindUser(_institution.AdminRepository.Administrators, email, password);
            if (_institution.CurrentUser != null)
            {
                _navigationStore.CurrentViewModel = new AdminMainPageViewModel();
                return true;
            }

            return false;
        }
    }
}
