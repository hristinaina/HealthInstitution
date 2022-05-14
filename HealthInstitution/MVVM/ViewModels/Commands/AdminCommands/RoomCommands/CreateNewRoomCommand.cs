using HealthInstitution.Commands;
using HealthInstitution.Exceptions.AdminExceptions;
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



        public override void Execute(object parameter)
        {
            int id = Institution.Instance().RoomRepository.GetID();
            try
            {
                Institution.Instance().RoomRepository.CreateRoom(id, _model.NewRoomName, _model.NewRoomNumber, (RoomType)_model.NewRoomType);
                _model.DialogOpen = false;
                _model.FillRoomList();
                _model.NewRoomNumber = 0;
                _model.NewRoomType = 0;
                _model.NewRoomName = null;
            } catch(ZeroRoomNumberException e)
            {
                _model.ShowMessage(e.Message);
            } catch(EmptyRoomNameException e)
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
