using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IRoomRenovationRepository : IRepository
    {
        public List<RoomRenovation> GetRooms();

        public void SetRooms(List<RoomRenovation> rooms);
    }
}