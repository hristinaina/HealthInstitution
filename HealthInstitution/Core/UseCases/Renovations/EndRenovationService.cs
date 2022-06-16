using System.Linq;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services.Renovations
{
    class EndRenovationService
    {
        private IRoomRepositoryService _rooms;

        public EndRenovationService()
        {
            _rooms = new RoomRepositoryService();
        }

        private void MergeRooms(Renovation renovation)
        {
            Room resultingRoom = renovation.Result[0];

            //rooms are deleted
            foreach (Room r in renovation.RoomsUnderRenovation)
            {
                _rooms.GetCurrentRooms().Remove(r);
                _rooms.GetDeletedRooms().Add(r);
            }

            _rooms.GetFutureRooms().Remove(resultingRoom);
            _rooms.GetCurrentRooms().Add(resultingRoom);
        }

        private void DivideRooms(Renovation renovation)
        {
            Room roomUnderRenovation = renovation.RoomsUnderRenovation[0];
            _rooms.GetCurrentRooms().Remove(roomUnderRenovation);
            _rooms.GetDeletedRooms().Add(roomUnderRenovation);

            foreach (Room r in renovation.Result)
            {
                _rooms.GetFutureRooms().Remove(r);
                _rooms.GetCurrentRooms().Add(r);
            }
        }

        public void EndRenovation(Renovation renovation)
        {
            if (renovation.RoomsUnderRenovation.Count() > 1)
            {
                MergeRooms(renovation);
            }
            else if (renovation.Result.Count() > 1)
            {
                DivideRooms(renovation);
            }
            else
            {
                if (renovation.RoomsUnderRenovation.Count > 0)
                    renovation.RoomsUnderRenovation[0].UnderRenovation = false;
            }
        }
    }
}
