using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
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

            Equipment equipment = Institution.Instance().EquipmentRepository.FindById(int.Parse(_viewModel.SelectedEquipment.Id));
            int quantity = int.Parse(_viewModel.Quantity);

            // TODO: create EquipmentOrder
            // TODO: napraviti fju koja kad prodje datum, dodaje te materijale u magacin (pogledati kako je Nemanja radio)

            _viewModel.FillEquipmentList();
        }

        private bool ValidateData()
        {
            if (_viewModel.SelectedEquipment == null)
            {
                MessageBox.Show("Please select equipment!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            bool isQuantityInt = Int32.TryParse(_viewModel.Quantity, out int quantity);
            if (!isQuantityInt)
            {
                MessageBox.Show("Quantity must be a number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
