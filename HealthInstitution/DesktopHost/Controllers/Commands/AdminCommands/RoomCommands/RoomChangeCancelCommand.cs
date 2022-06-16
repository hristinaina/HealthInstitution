using HealthInstitution.Commands;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands.RoomCommands
{
    class RoomChangeCancelCommand : BaseCommand
    {
        private AdminRoomViewModel _model;

        public RoomChangeCancelCommand(AdminRoomViewModel model)
        {
            _model = model;
        }

        public override void Execute(object parameter)
        {
            _model.DialogOpen = false;

            _model.SelectedType = _model.SelectedRoom.Type;
            _model.SelectedName = _model.SelectedRoom.Name;
            _model.SelectedNumber = _model.SelectedRoom.Number;
        }
    }
}
