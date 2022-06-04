using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services.Rooms
{
    class FilterRoomService
    {
        private RoomRepository _rooms;

        public FilterRoomService()
        {
            _rooms = Institution.Instance().RoomRepository;
        }

        public List<Entities.Room> FilterByRoomType(RoomType type)
        {
            List<Entities.Room> filteredRooms = new();
            foreach (Entities.Room r in _rooms.Rooms)
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }
    }
}
