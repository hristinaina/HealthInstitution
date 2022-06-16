using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface IFilterRoomService
    {
        public List<Room> FilterByRoomType(RoomType type);
    }
}