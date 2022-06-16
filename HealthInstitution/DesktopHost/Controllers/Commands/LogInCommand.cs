using HealthInstitution.Core;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;
using HealthInstitution.MVVM.ViewModels;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.MVVM.ViewModels.PatientViewModels;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.Commands
{
    public class LogInCommand : BaseCommand
    {
        private IDoctorRepositoryService _doctorService;
        private readonly Institution _institution;
        private readonly NavigationStore _navigationStore;
        private readonly LogInViewModel _loginVM;
        private IPatientRepositoryService _patientService;

        public LogInCommand(LogInViewModel loginVM)
        {
            _doctorService = new DoctorRepositoryService();
            _patientService = new PatientRepositoryService();
            _loginVM = loginVM;
            _institution = Institution.Instance();
            _navigationStore = NavigationStore.Instance();
        }

        public override void Execute(object parameter)
        {
            if (string.IsNullOrEmpty(_loginVM.Email) | string.IsNullOrEmpty(_loginVM.Password))
            {
                _loginVM.ShowMessage("All fields must be filled !");
                return;
            }

            try
            {
                bool foundUser = Login(_loginVM.Email, _loginVM.Password);

                if (!foundUser)
                {
                    _loginVM.ShowMessage("Wrong credentials !");
                }
            }
            catch (PatientBlockedException e)
            {
                _loginVM.ShowMessage(e.Message);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }


        // check which user type it is and redirect to the corresponding main page
        private bool Login(string email, string password)
        {
            _institution.CurrentUser = User.FindUser(_patientService.GetPatients(), email, password);
            if (_institution.CurrentUser != null)
            {
                Patient user = (Patient)_institution.CurrentUser;
                if (user.Blocked)
                {
                    throw new PatientBlockedException("Patient is blocked !");
                }

                if (user.Deleted)
                {
                    return false;
                }

                _navigationStore.CurrentViewModel = new PatientAppointmentViewModel();
                return true;
            }

            _institution.CurrentUser = User.FindUser(_doctorService.GetDoctors(), email, password);
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
                _navigationStore.CurrentViewModel = new AdminRoomViewModel();
                return true;
            }

            return false;
        }
    }
}
