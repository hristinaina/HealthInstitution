using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IRoomRepositoryService
    {
        public Room FindById(int id);

        public bool CheckNumber(int number, List<int> ignore);

        public int GetNewID();

        public Room AddRoom(Room room, bool future = false, List<int> ignoredNumbers = null);

        public void DeleteRoom(Room r);

        public List<Room> GetCurrentRooms();
        public List<Room> GetDeletedRooms();
        public List<Room> GetFutureRooms();
    }
}