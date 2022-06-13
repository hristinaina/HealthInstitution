using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.DoctorViewModels;
using HealthInstitution.Core.Services;

namespace HealthInstitution.MVVM.ViewModels.Commands.DoctorCommands
{
    class AcceptPendingMedicineCommand : BaseCommand
    {
        private DoctorPendingMedicineViewModel _viewModel;

        public AcceptPendingMedicineCommand(DoctorPendingMedicineViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DialogOpen = false;
            _viewModel.EnableChanges = false;

            PendingMedicine pendingMedicine = _viewModel.SelectedMedicine.PendingMedicine;

            try
            {
                DoctorPendingMedicineService service = new();
                bool isRemoved = service.Delete(pendingMedicine);
                if (isRemoved)
                {
                    Medicine medicine = new Medicine(pendingMedicine.ID, pendingMedicine.Name, pendingMedicine.Ingredients);
                    Institution.Instance().MedicineRepository.Add(medicine);

                    _viewModel.ShowMessage("Medicine successfully saved !");
                }
            }
            catch (Exception e)
            {
                _viewModel.ShowMessage(e.Message);
            }

            _viewModel.FindPendingMedicines();
             
        }
    }
}
