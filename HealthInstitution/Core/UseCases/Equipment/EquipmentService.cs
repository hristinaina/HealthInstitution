using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services.Equipments
{
    public class EquipmentService
    {
        private IEquipmentArrangementRepositoryService _arrangements;
        private IRoomRepositoryService _rooms;

        public EquipmentService()
        {
            _arrangements = new EquipmentArrangementRepositoryService();
            _rooms = new RoomRepositoryService();
        }

        public int GetQuantityInRoom(Room r, Equipment equipment)
        {
            return equipment.ArrangmentByRooms[r];
        }

    }
}