using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.Commands;

namespace HealthInstitution.MVVM.ViewModels.MainPageViewModels
{
    public class AdminMainPageViewModel : BaseViewModel
    {
        protected Admin admin;
        public ICommand LogOut { get; }

        public AdminMainPageViewModel(Admin admin)
        {
            this.admin = admin;
            LogOut = new LogOutCommand();

            // ..............
        }

    }
}
