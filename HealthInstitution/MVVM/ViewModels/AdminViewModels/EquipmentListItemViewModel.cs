using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.AdminViewModels
{
    public class EquipmentListItemViewModel : BaseViewModel
    {
        Equipment _equipment;
        Room _room;

        public Equipment Equipment { get => _equipment; set => _equipment = value; }
        public Room Room { get => _room; set => _room = value; }

        public string Name => _equipment.Name;
        public string RoomNumber { get; set; }
        public string Quantity => _equipment.ArrangmentByRooms[_room].ToString();
        public string SpecialMark => _equipment.ArrangmentByRooms[_room] == 0 ? "*" : " ";

        public EquipmentListItemViewModel(Equipment equipment, Room room)
        {
            _equipment = equipment;
            _room = room;

            RoomNumber = _room.Number.ToString();
            if (_room.Number == 0) RoomNumber = "Warehouse";

        }
    }
}
