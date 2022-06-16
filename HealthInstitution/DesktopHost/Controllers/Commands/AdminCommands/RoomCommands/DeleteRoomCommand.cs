using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Core.Repository;

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
            try
            {
                _model.DialogOpen = false;

                IRoomRepositoryService roomRepository = new RoomRepositoryService();
                roomRepository.DeleteRoom(_model.SelectedRoom.Room);

                _model.FillRoomList();
            } catch (RoomCannotBeChangedException e)
            {
                _model.ShowMessage(e.Message);
            } catch (Exception e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
