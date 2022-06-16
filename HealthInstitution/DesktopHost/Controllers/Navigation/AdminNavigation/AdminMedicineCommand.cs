using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.Core
{
    class AdminMedicineCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigation;

        public AdminMedicineCommand()
        {
            _institution = Institution.Instance();
            _navigation = NavigationStore.Instance();
        }
        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new AdminMedicineViewModel();
        }
    }
}
