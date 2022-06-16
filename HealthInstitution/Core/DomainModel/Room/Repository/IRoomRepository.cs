using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IRoomRepository
    {
        public Room FindById(int id);

        public bool CheckNumber(int number, List<int> ignore);

        public int GetID();

        public Room AddRoom(Room room, bool future = false, List<int> ignoredNumbers = null);

        public void DeleteRoom(Room r);
    }
}