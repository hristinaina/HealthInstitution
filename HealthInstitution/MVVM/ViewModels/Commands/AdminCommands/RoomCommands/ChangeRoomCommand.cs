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
using HealthInstitution.MVVM.Models.Services.Rooms;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RoomCommands
{
    class ChangeRoomCommand : BaseCommand
    {
        private AdminRoomViewModel _model;

        public ChangeRoomCommand(AdminRoomViewModel model)
        {
            _model = model;
        }

        private bool CheckPrerequisites()
        {
            bool prerequisitesFulfillled = true;
            int selectedNumber;
            if (!int.TryParse(_model.SelectedNumber, out selectedNumber))
            {
                _model.ShowMessage("Room number must be whole number");
                prerequisitesFulfillled = false;
            }
            return prerequisitesFulfillled;
        }

        public override void Execute(object parameter)
        {
            if (CheckPrerequisites())
            {
                try
                {
                    int selectedNumber = int.Parse(_model.SelectedNumber);
                    RoomService room = new RoomService(_model.SelectedRoom.Room);
                    room.Change(_model.SelectedName, selectedNumber, (RoomType)_model.SelectedTypeIndex);

                    _model.DialogOpen = false;
                    _model.FillRoomList();
                }
                catch (ZeroRoomNumberException e)
                {
                    _model.ShowMessage(e.Message);
                }
                catch (EmptyNameException e)
                {
                    _model.ShowMessage(e.Message);
                }
                catch (RoomNumberAlreadyTakenException e)
                {
                    _model.ShowMessage(e.Message);
                }
                catch (RoomCannotBeChangedException e)
                {
                    _model.ShowMessage(e.Message);
                }
                catch (Exception e)
                {
                    _model.ShowMessage(e.Message);
                }


            }
        }
    }
}
