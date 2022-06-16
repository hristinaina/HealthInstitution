using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services.Equipments
{
    public class FilterEquipmentService
    {
        private IEquipmentRepositoryService _equipment;

        public FilterEquipmentService()
        {
            _equipment = new EquipmentRepositoryService();
        }

        private Dictionary<Equipment, List<Room>> FilterByRoomType(RoomType type)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = new Dictionary<Equipment, List<Room>>();

            foreach (Equipment e in _equipment.GetEquipment())
            {
                foreach (Room r in e.ArrangmentByRooms.Keys)
                {
                    if (r.Type == type)
                    {
                        if (!filteredEquipment.ContainsKey(e)) filteredEquipment.Add(e, new List<Room>());
                        filteredEquipment[e].Add(r);
                    }
                }
            }
            return filteredEquipment;
        }

        private Dictionary<Equipment, List<Room>> FilterByEquipmentType(Dictionary<Equipment, List<Room>> allEquipment, EquipmentType type)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = new();

            foreach (Equipment e in allEquipment.Keys)
            {
                if (e.Type == type)
                {
                    if (!filteredEquipment.ContainsKey(e)) filteredEquipment.Add(e, new List<Room>());
                    filteredEquipment[e] = allEquipment[e];
                }
            }
            return filteredEquipment;
        }

        private Dictionary<Equipment, List<Room>> FilterByQuantity(Dictionary<Equipment, List<Room>> allEquipment, int minQuantity, int maxQuantity)
        {
            if (minQuantity >= maxQuantity) throw new EquipmentFilterQuantityException("Minimum quantity must be lower than maximum quantity");
            Dictionary<Equipment, List<Room>> filteredEquipment = new();
            foreach (Equipment e in allEquipment.Keys)
            {
                foreach (Room r in allEquipment[e])
                {
                    if (e.ArrangmentByRooms[r] >= minQuantity && e.ArrangmentByRooms[r] <= maxQuantity)
                    {
                        if (!filteredEquipment.ContainsKey(e)) filteredEquipment.Add(e, new List<Room>());
                        filteredEquipment[e].Add(r);
                    }
                }
            }
            return filteredEquipment;
        }

        public Dictionary<Equipment, List<Room>> Filter(RoomType roomType, int minQuantity, int maxQuantity, EquipmentType equipmentType)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = FilterByRoomType(roomType);

            filteredEquipment = FilterByQuantity(filteredEquipment, minQuantity, maxQuantity);

            filteredEquipment = FilterByEquipmentType(filteredEquipment, equipmentType);

            return filteredEquipment;
        }
    }
}