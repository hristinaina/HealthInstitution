using System;
using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.MVVM.ViewModels.SecretaryViewModels;

namespace HealthInstitution.MVVM.ViewModels.Commands.SecretaryCommands.EquipmentCommands
{
    public class OrderEquipmentCommand : BaseCommand
    {
        private readonly Institution _institution;
        private OrderingEquipmentViewModel _viewModel;

        public OrderEquipmentCommand(OrderingEquipmentViewModel viewModel)
        {
            _institution = Institution.Instance();
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            bool validation = ValidateData();
            if (!validation) return;

            IEquipmentRepositoryService equipmentRepository = new EquipmentRepositoryService();
            Equipment equipment = equipmentRepository.FindByID(int.Parse(_viewModel.SelectedEquipment.Id));
            int quantity = int.Parse(_viewModel.Quantity);

            IEquipmentOrderRepositoryService equipmentOrderRepository = new EquipmentOrderRepositoryService();
            equipmentOrderRepository.CreateOrder(equipment, quantity);
            _viewModel.ShowMessage("Equipment has been successfully ordered!");
            _viewModel.DialogOpen = false;

            _viewModel.FillEquipmentList();
        }

        private bool ValidateData()
        {
            if (_viewModel.SelectedEquipment == null)
            {
                _viewModel.ShowMessage("Please select equipment!");
                return false;
            }

            bool isQuantityInt = Int32.TryParse(_viewModel.Quantity, out int quantity);
            if (!isQuantityInt)
            {
                _viewModel.ShowMessage("Quantity must be a number!");
                return false;
            }

            return true;
        }
    }
}
