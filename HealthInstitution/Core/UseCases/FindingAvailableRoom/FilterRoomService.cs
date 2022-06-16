using System.Collections.Generic;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    class FilterRoomService : IFilterRoomService
    {
        private IRoomRepositoryService _rooms;

        public FilterRoomService()
        {
            _rooms = new RoomRepositoryService();
        }

        public List<Room> FilterByRoomType(RoomType type)
        {
            List<Room> filteredRooms = new();
            foreach (Room r in _rooms.GetCurrentRooms())
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }
    }
}
