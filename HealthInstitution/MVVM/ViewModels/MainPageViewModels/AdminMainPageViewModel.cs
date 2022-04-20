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
        protected Admin admin;

        public AdminMainPageViewModel(Admin admin)
        {
            this.admin = admin;

            // ..............
        }

    }
}
