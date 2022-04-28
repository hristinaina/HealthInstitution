using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class AdminMainPageViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Admin _admin;
        public ICommand LogOut { get; }

        public AdminMainPageViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            LogOut = new LogOutCommand();

            // ..............
        }

    }
}
