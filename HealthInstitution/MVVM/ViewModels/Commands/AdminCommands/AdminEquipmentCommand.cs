using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands
{
    class AdminEquipmentCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigation;

        public AdminEquipmentCommand()
        {
            _institution = Institution.Instance();
            _navigation = NavigationStore.Instance();
        }
        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new AdminEquipmentViewModel();
        }
    }
}
