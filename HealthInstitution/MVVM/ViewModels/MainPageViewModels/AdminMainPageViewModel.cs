using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class AdminMainPageViewModel : BaseViewModel
    {
        protected AdminController admin;

        public AdminMainPageViewModel(AdminController admin)
        {
            this.admin = admin;

            // ..............
        }

    }
}
