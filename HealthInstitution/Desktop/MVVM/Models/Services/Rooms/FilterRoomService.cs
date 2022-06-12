using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;

namespace HealthInstitution.Core.Services.Rooms
{
    class FilterRoomService
    {
        private RoomRepository _rooms;

        public FilterRoomService()
        {
            _rooms = Institution.Instance().RoomRepository;
        }

        public List<Room> FilterByRoomType(RoomType type)
        {
            List<Room> filteredRooms = new();
            foreach (Room r in _rooms.Rooms)
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }
    }
}
