using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IRoomRenovationRepositoryService
    {
        public List<RoomRenovation> GetRooms();
    }
}