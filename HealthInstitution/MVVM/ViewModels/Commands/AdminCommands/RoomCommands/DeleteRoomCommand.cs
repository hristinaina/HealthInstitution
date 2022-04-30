using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands
{
    class DeleteRoomCommand : BaseCommand
    {
        private AdminRoomViewModel _model;

        public DeleteRoomCommand(AdminRoomViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            if (!_model.SelectedRoom.Room.IsDeletable())
            {
                MessageBox.Show("Room cannot be deleted, because it has scheduled appointments", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else{
                _model.DialogOpen = false;

                Institution.Instance().RoomRepository.Rooms.Remove(_model.SelectedRoom.Room);
                _model.FillRoomList();
            }
        }
    }
}
