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

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands
{
    class CreateNewRoomCommand : BaseCommand
    {
        private AdminRoomViewModel _model;

        public CreateNewRoomCommand(AdminRoomViewModel model)
        {
            _model = model;
        }

        private bool CheckPrerequisites()
        {
            bool prerequisitesFulfillled = true;

            if (_model.NewRoomName is null)
            {
                MessageBox.Show("You need to fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfillled = false;
            }
            else if (_model.NewRoomNumber == 0)
            {
                MessageBox.Show("Room number cannot be 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfillled = false;
            }
            else if (!Institution.Instance().RoomRepository.CheckNumber(_model.NewRoomNumber))
            {
                MessageBox.Show("Number already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfillled = false;
            }

            return prerequisitesFulfillled;
        }

        public override void Execute(object parameter)
        {
            
            if (CheckPrerequisites())
            {
                _model.DialogOpen = false;

                int id = Institution.Instance().RoomRepository.GetID();
                Institution.Instance().RoomRepository.CreateRoom(id, _model.NewRoomName, _model.NewRoomNumber, (RoomType)_model.NewRoomType);
                _model.FillRoomList();
            }
        }
    }
}
