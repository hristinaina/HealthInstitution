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
    class CreateNewRoomCommand : BaseCommand
    {
        private AdminRoomViewModel _model;

        public CreateNewRoomCommand(AdminRoomViewModel model)
        {
            _model = model;
        }



        public override void Execute(object parameter)
        {
            try
            {
                Room r = new Room(_model.NewRoomName, _model.NewRoomNumber, (RoomType)_model.NewRoomType);
                IRoomRepositoryService roomRepository = new RoomRepositoryService();
                roomRepository.AddRoom(r);
                _model.DialogOpen = false;
                _model.FillRoomList();
                _model.NewRoomNumber = 0;
                _model.NewRoomType = 0;
                _model.NewRoomName = null;
            } catch(ZeroRoomNumberException e)
            {
                _model.ShowMessage(e.Message);
            } catch(EmptyNameException e)
            {
                _model.ShowMessage(e.Message);
            } catch(RoomNumberAlreadyTakenException e)
            {
                _model.ShowMessage(e.Message);
            } catch (Exception e)
            {
                _model.ShowMessage(e.Message);
            }
        }
    }
}
