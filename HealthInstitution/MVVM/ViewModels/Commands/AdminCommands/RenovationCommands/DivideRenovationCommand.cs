using HealthInstitution.Commands;
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
using HealthInstitution.Exceptions.AdminExceptions;

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
                MessageBox.Show("Room for dividing must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.FirstNewRoomNumber == _model.SecondNewRoomNumber)
            {
                MessageBox.Show("New room numbers must be different", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.StartDate <= DateTime.Today)
            {
                MessageBox.Show("Start date must be in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.EndDate <= DateTime.Today)
            {
                MessageBox.Show("End date must be in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.StartDate >= _model.EndDate)
            {
                MessageBox.Show("End date must be after start date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            } else if (_model.SelectedRoom.IsUnderRenovation(_model.StartDate, _model.EndDate))
            {
                MessageBox.Show("Room is already under renovation in selected period", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    int id = Institution.Instance().RoomRepository.GetID();
                    List<Room> roomUnderRenovation = new List<Room> { _model.SelectedRoom};
                    Room firstResultingRoom = Institution.Instance().RoomRepository.CreateRoom(Institution.Instance().RoomRepository.GetID(), _model.FirstNewRoomName, _model.FirstNewRoomNumber, (RoomType)_model.FirstNewRoomType, true, new List<int> { _model.SelectedRoom.Number });
                    Institution.Instance().RoomRepository.FutureRooms.Add(firstResultingRoom);

                    id = Institution.Instance().RoomRepository.GetID();
                    Room secondResultingRoom = Institution.Instance().RoomRepository.CreateRoom(id, _model.SecondNewRoomName, _model.SecondNewRoomNumber, (RoomType)_model.SecondNewRoomType, true, new List<int> { _model.SelectedRoom.Number });
                
                    List<Room> result = new List<Room> { firstResultingRoom, secondResultingRoom };
                    Renovation renovation = new Renovation(Institution.Instance().RenovationRepository.GetID(), _model.StartDate, _model.EndDate, roomUnderRenovation, result);
                    Institution.Instance().RenovationRepository.Renovations.Add(renovation);

                    Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, _model.SelectedRoom.ID, false));
                    Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, firstResultingRoom.ID, true));
                    Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, secondResultingRoom.ID, true));


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
                catch (Exception e)
                {
                    _model.ShowMessage(e.Message);
                }
            }
        }
    }
}
