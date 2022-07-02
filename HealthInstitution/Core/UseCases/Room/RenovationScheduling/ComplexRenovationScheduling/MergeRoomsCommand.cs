using HealthInstitution.Commands;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands
{
    class MergeRoomsCommand : BaseCommand
    {

        public MergeRoomsCommand()
        {
        }

        public override void Execute(object parameter)
        {
            NavigationStore.Instance().CurrentViewModel = new AdminMergeComplexRenovationViewModel();
        }
    }
}
