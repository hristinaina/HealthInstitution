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
            else if (_model.FirstNewRoomName is null || _model.FirstNewRoomName.Equals("") || _model.SecondNewRoomName is null || _model.SecondNewRoomName.Equals(""))
            {
                MessageBox.Show("You need to fill new room names", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.FirstNewRoomNumber == 0 || _model.SecondNewRoomNumber == 0)
            {
                MessageBox.Show("New room numbers cannot be 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.FirstNewRoomNumber == _model.SecondNewRoomNumber)
            {
                MessageBox.Show("New room numbers must be different", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (!Institution.Instance().RoomRepository.CheckNumber(_model.FirstNewRoomNumber) && !Institution.Instance().RoomRepository.CheckNumber(_model.SecondNewRoomNumber)
                && _model.SelectedRoom.Number != _model.FirstNewRoomNumber && _model.SelectedRoom.Number != _model.SecondNewRoomNumber)
            {
                MessageBox.Show("Number already taken", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            }

            return prerequisitesFulfilled;
        }

        public override void Execute(object parameter)
        {
            if (!CheckPrerequisites())
            {
                List<Room> rooms = new List<Room> { _model.SelectedRoom};
                Room firstResultingRoom = new Room(Institution.Instance().RoomRepository.GetID(), _model.FirstNewRoomNumber, _model.FirstNewRoomName, (RoomType)_model.FirstNewRoomType);
                Institution.Instance().RoomRepository.FutureRooms.Add(firstResultingRoom);

                Room secondResultingRoom = new Room(Institution.Instance().RoomRepository.GetID(), _model.SecondNewRoomNumber, _model.SecondNewRoomName, (RoomType)_model.SecondNewRoomType);
                Institution.Instance().RoomRepository.FutureRooms.Add(secondResultingRoom);
                
                List<Room> result = new List<Room> { firstResultingRoom, secondResultingRoom };
                Renovation r = new Renovation(Institution.Instance().RenovationRepository.GetID(), _model.StartDate, _model.EndDate, rooms, result);
                Institution.Instance().RenovationRepository.Renovations.Add(r);

                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(r.ID, _model.SelectedRoom.ID, false));
                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(r.ID, firstResultingRoom.ID, true));
                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(r.ID, secondResultingRoom.ID, true));


                NavigationStore.Instance().CurrentViewModel = new AdminRenovationViewModel();
            }
        }
    }
}
