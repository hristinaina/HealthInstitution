using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class DeletePendingMedicineCommand : BaseCommand
    {
        private DoctorPendingMedicineViewModel _viewModel;
        public DeletePendingMedicineCommand(DoctorPendingMedicineViewModel pendingMedicineViewModel)
        {
            _viewModel = pendingMedicineViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            _viewModel.EnableChanges = false;

            try
            {
                bool isDeleted = Institution.Instance().DeletePendingMedicine((PendingMedicine)_viewModel.SelectedMedicine.PendingMedicine);

                if (isDeleted) _viewModel.ShowMessage("Medicine suggestion successfully deleted !");
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }

            _viewModel.FindPendingMedicines();

        }
    }
}
