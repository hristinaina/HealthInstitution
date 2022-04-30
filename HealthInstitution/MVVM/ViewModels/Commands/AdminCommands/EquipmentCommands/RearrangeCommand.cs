using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.EquipmentCommands
{
    class RearrangeCommand : BaseCommand
    {
        private AdminEquipmentViewModel _model;

        public RearrangeCommand(AdminEquipmentViewModel model)
        {
            _model = model;
        }

        private bool CheckPrerequisites()
        {
            bool prerequisitesFulfilled = true;
            if (_model.NewArrangementStartDate is null || _model.NewArrangementQuantity == 0 || _model.NewArrangemenTargetRoom is null)
            {
                MessageBox.Show("All fields must be filled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.ParseDate(_model.NewArrangementStartDate) <= DateTime.Today)
            {
                MessageBox.Show("Arrangement date must be in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.NewArrangementQuantity > _model.SelectedEquipment.Equipment.ArrangmentByRooms[_model.SelectedEquipment.Room])
            {
                MessageBox.Show("Not enough equipment in selected room", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            return prerequisitesFulfilled;
        }

        public override void Execute(object parameter)
        {
            if (CheckPrerequisites())
            {
                _model.DialogOpen = false;

                DateTime newArrangementStartDate = _model.ParseDate(_model.NewArrangementStartDate);
                EquipmentArrangement destinationRoomArrangement = Institution.Instance().EquipmentArragmentRepository.FindByRoomAndEquipment(_model.SelectedEquipment.Room, _model.SelectedEquipment.Equipment);
                destinationRoomArrangement.EndDate = newArrangementStartDate;
                EquipmentArrangement targetRoomArrangement = Institution.Instance().EquipmentArragmentRepository.FindByRoomAndEquipment(_model.NewArrangemenTargetRoom, _model.SelectedEquipment.Equipment);
                if (targetRoomArrangement is not null) targetRoomArrangement.EndDate = newArrangementStartDate;


                int newDestinationRoomQuantity = destinationRoomArrangement.Quantity - _model.NewArrangementQuantity;
                int newTargetRoomQuantity = 0;
                if (targetRoomArrangement is not null)
                {
                    newTargetRoomQuantity = targetRoomArrangement.Quantity;
                }
                newTargetRoomQuantity += _model.NewArrangementQuantity;


                Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Add(new EquipmentArrangement(_model.SelectedEquipment.Equipment, _model.SelectedEquipment.Room, newDestinationRoomQuantity, newArrangementStartDate, DateTime.MaxValue));
                Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Add(new EquipmentArrangement(_model.SelectedEquipment.Equipment, _model.NewArrangemenTargetRoom, newTargetRoomQuantity, newArrangementStartDate, DateTime.MaxValue));

                MessageBox.Show("Arrangement successfully planned", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
