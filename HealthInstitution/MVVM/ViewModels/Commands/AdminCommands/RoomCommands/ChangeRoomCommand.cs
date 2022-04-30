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

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RoomCommands
{
    class ChangeRoomCommand : BaseCommand
    {
        private AdminRoomViewModel _model;

        public ChangeRoomCommand(AdminRoomViewModel model)
        {
            _model = model;
        }
        public override void Execute(object parameter)
        {
            int selectedNumber = 0;
            if (_model.SelectedRoom.Room.Type != (RoomType)_model.SelectedTypeIndex && !_model.SelectedRoom.Room.IsChangeble())
            {
                MessageBox.Show("Room cannot be changed, because it has scheduled appointments", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _model.SelectedType = _model.SelectedRoom.Type;
                _model.SelectedName = _model.SelectedRoom.Name;
                _model.SelectedNumber = _model.SelectedRoom.Number;
            } else if (!int.TryParse(_model.SelectedNumber, out selectedNumber))
            {
                MessageBox.Show("Room number must be whole number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _model.SelectedType = _model.SelectedRoom.Type;
                _model.SelectedName = _model.SelectedRoom.Name;
                _model.SelectedNumber = _model.SelectedRoom.Number;
            }
            else if (!Institution.Instance().RoomRepository.CheckNumber(selectedNumber))
            {
                MessageBox.Show("Number already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                _model.DialogOpen = false;

                _model.SelectedRoom.Room.Name = _model.SelectedName;
                _model.SelectedRoom.Room.Number = selectedNumber;
                _model.SelectedRoom.Room.Type = (RoomType)_model.SelectedTypeIndex;
                _model.FillRoomList();
            }
        }
    }
}
