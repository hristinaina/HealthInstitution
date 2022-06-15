using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    class RoomListItemViewModel : BaseViewModel
    {

        private Room _room;

        public Room Room { get => _room; set => _room = value; }

        public string ID => _room.ID.ToString();
        public string Name => _room.Name;
        public string Number => _room.Number.ToString();
        public string Type => _room.Type.ToString();

        public RoomListItemViewModel(Room room)
        {
            _room = room;
        }
    }
}
