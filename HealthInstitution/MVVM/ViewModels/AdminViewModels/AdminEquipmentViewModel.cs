using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class AdminEquipmentViewModel : BaseViewModel
    {
        private Institution _institution;
        private Admin _admin;

        public AdminNavigationViewModel Navigation { get; }

        public AdminEquipmentViewModel()
        {
            _institution = Institution.Instance();
            _admin = (Admin)_institution.CurrentUser;
            Navigation = new AdminNavigationViewModel();
            // ..............
        }
    }
}
