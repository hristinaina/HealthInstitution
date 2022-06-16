using HealthInstitution.Commands;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RenovationCommands
{
    class MergeRenovationCommand : BaseCommand
    {
        private AdminMergeComplexRenovationViewModel _model;

        public MergeRenovationCommand(AdminMergeComplexRenovationViewModel model)
        {
            _model = model;
        }

        private bool CheckPrerequisites()
        {
            bool prerequisitesFulfilled = true;
            if (_model.FirstSelectedRoom is null || _model.SecondSelectedRoom is null)
            {
                _model.ShowMessage("Rooms for merging must be selected");
                prerequisitesFulfilled = false;
            }
            else if (_model.FirstSelectedRoom == _model.SecondSelectedRoom)
            {
                _model.ShowMessage("Rooms for merging must be different");
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
                    IRoomRepositoryService roomRepository = new RoomRepositoryService();
                    IRenovationRepositoryService renovationRepository = new RenovationRepositoryService();

                    Renovation renovation = renovationRepository.Create(_model.StartDate, _model.EndDate);
                    
                    List<Room> roomsUnderRenovation = new List<Room> { _model.FirstSelectedRoom, _model.SecondSelectedRoom };

                    List<int> numbersForIgnoring = new List<int> { _model.FirstSelectedRoom.Number, _model.SecondSelectedRoom.Number };

                    Room room = new Room(_model.NewRoomName, _model.NewRoomNumber, (RoomType)_model.NewRoomType);
                    Room resultingRoom = roomRepository.AddRoom(room, true, numbersForIgnoring);
                    
                    List<Room> result = new List<Room> { resultingRoom };

                    renovation.RoomsUnderRenovation = roomsUnderRenovation;
                    renovation.Result = result;

                    renovationRepository.GetRenovations().Add(renovation);
                
                    _model.FirstSelectedRoom.Renovations.Add(renovation);
                    _model.SecondSelectedRoom.Renovations.Add(renovation);

                    IRoomRenovationRepositoryService roomRenovations = new RoomRenovationRepositoryService();

                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, _model.FirstSelectedRoom.ID, false));
                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, _model.SecondSelectedRoom.ID, false));
                    roomRenovations.GetRooms().Add(new RoomRenovation(renovation.ID, resultingRoom.ID, true));

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
