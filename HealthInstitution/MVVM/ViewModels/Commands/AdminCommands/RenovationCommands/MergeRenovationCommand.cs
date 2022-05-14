using HealthInstitution.Commands;
using HealthInstitution.Exceptions;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                    Renovation renovation = Institution.Instance().RenovationRepository.Create(_model.StartDate, _model.EndDate);
                    
                    List<Room> roomsUnderRenovation = new List<Room> { _model.FirstSelectedRoom, _model.SecondSelectedRoom };

                    int id = Institution.Instance().RoomRepository.GetID();
                    List<int> numbersForIgnoring = new List<int> { _model.FirstSelectedRoom.Number, _model.SecondSelectedRoom.Number };
                    Room resultingRoom = Institution.Instance().RoomRepository.CreateRoom(id, _model.NewRoomName, _model.NewRoomNumber, (RoomType)_model.NewRoomType, true, numbersForIgnoring);
                    List<Room> result = new List<Room> { resultingRoom };

                    renovation.RoomsUnderRenovation = roomsUnderRenovation;
                    renovation.Result = result;
                    Institution.Instance().RenovationRepository.Renovations.Add(renovation);
                
                    Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, _model.FirstSelectedRoom.ID, false));
                    Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, _model.SecondSelectedRoom.ID, false));
                    Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, resultingRoom.ID, true));

                    NavigationStore.Instance().CurrentViewModel = new AdminRenovationViewModel();
                }
                catch (ZeroRoomNumberException e)
                {
                    _model.ShowMessage(e.Message);
                }
                catch (EmptyRoomNameException e)
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
