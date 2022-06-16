using System.Collections.Generic;

namespace HealthInstitution.Core.Services.Equipments
{
    public interface IFilterEquipmentService
    {
        public Dictionary<Equipment, List<Room>> Filter(RoomType roomType, int minQuantity, int maxQuantity, EquipmentType equipmentType)
    }
}