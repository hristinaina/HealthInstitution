using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands
{
    class DivideRenovationCommand : BaseCommand
    {
        private AdminDivideComplexRenovationViewModel _model;

        public DivideRenovationCommand(AdminDivideComplexRenovationViewModel model)
        {
            _model = model;
        }

        private bool CheckPrerequisites()
        {
            bool prerequisitesFulfilled = true;

            if (_model.SelectedRoom is null)
            {
                _model.ShowMessage("Room for dividing must be selected");
                prerequisitesFulfilled = false;
            }
            else if (_model.FirstNewRoomNumber == _model.SecondNewRoomNumber)
            {
                _model.ShowMessage("New room numbers must be different");
                prerequisitesFulfilled = false;
            }
            return prerequisitesFulfilled;
        }

        public override void Execute(object parameter)
        {
            if (CheckPrerequisites())
            {
                try
                {
                    IRenovationRepositoryService renovationsService = new RenovationRepositoryService();
                    Renovation renovation = renovationsService.Create(_model.StartDate, _model.EndDate);

                    IRoomRepositoryService roomService = new RoomRepositoryService();

                    int id = roomService.GetNewID();
                    List<Room> roomUnderRenovation = new List<Room> { _model.SelectedRoom};

                    Room firstRoom = new Room(_model.FirstNewRoomName, _model.FirstNewRoomNumber,
                        (RoomType)_model.FirstNewRoomType);
                    Room firstResultingRoom = roomService.AddRoom(firstRoom, true, new List<int> { _model.SelectedRoom.Number });

                    Room secondRoom = new Room(_model.SecondNewRoomName, _model.SecondNewRoomNumber,
                        (RoomType)_model.SecondNewRoomType);
                    Room secondResultingRoom = roomService.AddRoom(secondRoom, true, new List<int> { _model.SelectedRoom.Number });
                

                    List<Room> result = new List<Room> { firstResultingRoom, secondResultingRoom };

                    renovation.Result = result;
                    renovation.RoomsUnderRenovation = roomUnderRenovation;

                    renovationsService.GetRenovations().Add(renovation);

                    _model.SelectedRoom.Renovations.Add(renovation);

                    IRoomRenovationRepositoryService roomRenovations = new RoomRenovationRepositoryService();

                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, _model.SelectedRoom.ID, false));
                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, firstResultingRoom.ID, true));
                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, secondResultingRoom.ID, true));


                    NavigationStore.Instance().CurrentViewModel = new AdminRenovationViewModel();
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
