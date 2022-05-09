using HealthInstitution.Commands;
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

        public override void Execute(object parameter)
        {
            if (_model.NewRenovationRoom is null)
            {
                MessageBox.Show("Room must be selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if (_model.NewRenovationStartDate <= DateTime.Today)
            {
                MessageBox.Show("Start date must be in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if (_model.NewRenovationEndDate <= DateTime.Today)
            {
                MessageBox.Show("End date must be in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else if (_model.NewRenovationStartDate >= _model.NewRenovationEndDate)
            {
                MessageBox.Show("End date must be after start date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
            _model.DialogOpen = false;
            }
        }
    }
}
