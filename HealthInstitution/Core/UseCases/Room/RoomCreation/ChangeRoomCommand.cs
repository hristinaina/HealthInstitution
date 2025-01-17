﻿using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RoomCommands
{
    class ChangeRoomCommand : BaseCommand
    {
        private AdminRoomViewModel _model;
        private IRoomRepositoryService _roomManager;

        public ChangeRoomCommand(AdminRoomViewModel model)
        {
            _model = model;
            _roomManager = new RoomRepositoryService();
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
                    _roomManager.Change(_model.SelectedName, selectedNumber, (RoomType)_model.SelectedTypeIndex, _model.SelectedRoom.Room);

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
