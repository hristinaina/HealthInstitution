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
                MessageBox.Show("Rooms for merging must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.NewRoomName is null || _model.NewRoomName.Equals(""))
            {
                MessageBox.Show("You need to fill new room name", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.NewRoomNumber == 0)
            {
                MessageBox.Show("New room number cannot be 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (!Institution.Instance().RoomRepository.CheckNumber(_model.NewRoomNumber) && _model.FirstSelectedRoom.Number != _model.NewRoomNumber && _model.SecondSelectedRoom.Number != _model.NewRoomNumber)
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
            if (CheckPrerequisites())
            {
                List<Room> rooms = new List<Room> { _model.FirstSelectedRoom, _model.SecondSelectedRoom };
                Room resultingRoom = new Room(Institution.Instance().RoomRepository.GetID(), _model.NewRoomNumber, _model.NewRoomName, (RoomType)_model.NewRoomType);
                List<Room> result = new List<Room> { resultingRoom };
                Renovation r = new Renovation(Institution.Instance().RenovationRepository.GetID(), _model.StartDate, _model.EndDate, rooms, result);
                Institution.Instance().RenovationRepository.Renovations.Add(r);
                
                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(r.ID, _model.FirstSelectedRoom.ID, false));
                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(r.ID, _model.SecondSelectedRoom.ID, false));
                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(r.ID, resultingRoom.ID, true));

                Institution.Instance().RoomRepository.FutureRooms.Add(resultingRoom);

                NavigationStore.Instance().CurrentViewModel = new AdminRenovationViewModel();
            }
        }
    }
}
