using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands
{
    class FilterCommand : BaseCommand
    {
        private AdminEquipmentViewModel _model;

        public FilterCommand(AdminEquipmentViewModel model)
        {
            _model = model;
        }
        public override void Execute(object parameter)
        {
            //_model.FilterEquipmentType;
            //_model.FilterRoomType;
            //_model.FilterMinQuantity;
            //_model.FilterMaxQuantity;

            int minQuantity = 0, maxQuantity = 0;
            if (!(int.TryParse(_model.FilterMinQuantity, out minQuantity) && int.TryParse(_model.FilterMaxQuantity, out maxQuantity)))
            {
                MessageBox.Show("Minimum and maximum quantity must be whole numbers!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if (minQuantity >= maxQuantity)
            {
                MessageBox.Show("Minimum quantity must be lower than maximum quantity!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                _model.DialogOpen = false;

                _model.FilteredEquipment = Institution.Instance().EquipmentRepository.FilterEquipment((RoomType)_model.FilterRoomType, minQuantity, maxQuantity, (EquipmentType)_model.FilterEquipmentType);

                _model.FilterEquipmentList();
            }
        }
    }
}
