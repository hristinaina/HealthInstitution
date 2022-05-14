using HealthInstitution.Commands;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                MessageBox.Show("Room must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.NewRenovationStartDate <= DateTime.Today)
            {
                MessageBox.Show("Start date must be in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.NewRenovationEndDate <= DateTime.Today)
            {
                MessageBox.Show("End date must be in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.NewRenovationStartDate >= _model.NewRenovationEndDate)
            {
                MessageBox.Show("End date must be after start date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                prerequisitesFulfilled = false;
            }
            else if (_model.NewRenovationRoom.IsUnderRenovation(_model.NewRenovationStartDate, _model.NewRenovationEndDate))
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
                _model.DialogOpen = false;
                
                List<Room> rooms = new List<Room> { _model.NewRenovationRoom };
                Renovation renovation = new Renovation(Institution.Instance().RenovationRepository.GetID(), _model.NewRenovationStartDate, _model.NewRenovationEndDate, rooms, rooms);
                Institution.Instance().RenovationRepository.Renovations.Add(renovation);
                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, _model.NewRenovationRoom.ID, false));
                Institution.Instance().RoomRenovationRepository.RoomsUnderRenovations.Add(new RoomRenovation(renovation.ID, _model.NewRenovationRoom.ID, true));

                _model.FillRenovationList();
            }
        }
    }
}
