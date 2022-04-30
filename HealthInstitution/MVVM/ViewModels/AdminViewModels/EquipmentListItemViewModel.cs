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
        public string Name => _equipment.Name;
        public string Room => _room.Number.ToString();
        public string Quantity => _equipment.ArrangmentByRooms[_room].ToString();

        public EquipmentListItemViewModel(Equipment equipment, Room room)
        {
            _equipment = equipment;
            _room = room;
        }
    }
}
