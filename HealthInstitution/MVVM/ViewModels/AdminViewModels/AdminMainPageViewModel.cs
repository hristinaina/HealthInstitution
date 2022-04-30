using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class AdminMainPageViewModel : BaseViewModel
    {
        private Institution _institution;
        protected Admin _admin;
        public AdminNavigationViewModel Navigation { get; }

        public AdminMainPageViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            Navigation = new AdminNavigationViewModel();

            // ..............
        }

    }
}
