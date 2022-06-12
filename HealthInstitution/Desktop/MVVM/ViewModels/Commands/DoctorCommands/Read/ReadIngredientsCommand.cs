using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Stores;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class ReadIngredientsCommand : BaseCommand
    {
        private readonly NavigationStore _navigationStore;
        private DoctorPendingMedicineViewModel _viewModel;

        public ReadIngredientsCommand(DoctorPendingMedicineViewModel viewModel)
        {
            _navigationStore = NavigationStore.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = true;
            _viewModel.FindIngredients(_viewModel.SelectedMedicine.PendingMedicine);
        }
    }
}
