using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands
{
    class ScheduleRenovationCommand : BaseCommand
    {
        private AdminRenovationViewModel _model;

        public ScheduleRenovationCommand(AdminRenovationViewModel model)
        {
            _model = model;
        }

        private bool CheckPrerequisites()
        {
            bool prerequisitesFulfilled = true;
            if (_model.NewRenovationRoom is null)
            {
                _model.ShowMessage("Room must be selected");
                prerequisitesFulfilled = false;
            }
            return prerequisitesFulfilled;
        }

        public override void Execute(object parameter)
        {
            if(CheckPrerequisites())
            {
                try
                {
                    IRenovationRepositoryService renovationRepository = new RenovationRepositoryService();

                    List<Room> roomUnderRenovation = new List<Room> { _model.NewRenovationRoom };
                    Renovation renovation = renovationRepository.Create(_model.NewRenovationStartDate, _model.NewRenovationEndDate);

                    renovation.RoomsUnderRenovation = roomUnderRenovation;
                    renovation.Result = roomUnderRenovation;

                    _model.NewRenovationRoom.Renovations.Add(renovation);

                
                    renovationRepository.GetRenovations().Add(renovation);

                    IRoomRenovationRepositoryService roomRenovations = new RoomRenovationRepositoryService();

                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, _model.NewRenovationRoom.ID, false));
                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, _model.NewRenovationRoom.ID, true));

                    _model.DialogOpen = false;
                    _model.FillRenovationList();
                }
                catch (DateException e)
                {
                    _model.ShowMessage(e.Message);
                }
                catch (RoomUnderRenovationException e)
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
