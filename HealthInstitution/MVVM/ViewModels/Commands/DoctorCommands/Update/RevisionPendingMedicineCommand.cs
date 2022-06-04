using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class RevisionPendingMedicineCommand : BaseCommand
    {
        private DoctorPendingMedicineViewModel _viewModel;
        public RevisionPendingMedicineCommand(DoctorPendingMedicineViewModel pendingMedicineViewModel)
        {
            _viewModel = pendingMedicineViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            _viewModel.SelectedMedicine.PendingMedicine.RevisionDescription = _viewModel.RevisionReason;
            _viewModel.SelectedMedicine.PendingMedicine.State = Models.Enumerations.State.REVISION;

            try
            {
                DoctorPendingMedicineService service = new();
                bool isOnRevision = service.SendToRevision((PendingMedicine)_viewModel.SelectedMedicine.PendingMedicine);

                if (isOnRevision) _viewModel.ShowMessage("Medicine suggestion successfully sent to revision !");
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }

            _viewModel.FindPendingMedicines();

        }
    }
}
