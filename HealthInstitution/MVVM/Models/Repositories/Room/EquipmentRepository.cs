using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Services;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.Models
{
    public class EquipmentRepository
    {
        private string _fileName;
        private List<Equipment> _equipment;

        public List<Equipment> Equipment { get => this._equipment; }

        public EquipmentRepository(string roomsFileName)
        {
            this._fileName = roomsFileName;
            this._equipment = new List<Equipment>();
        }

        public void LoadFromFile()
        {
            this._equipment = FileService.Deserialize<Equipment>(this._fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Equipment>(this._fileName, this._equipment);
        }

        public Equipment FindById(int id)
        {
            foreach (Equipment e in this._equipment)
            {
                if (e.ID == id) return e;
            }
            return null;
        }

        public List<Equipment> Search(string phrase)
        {
            List<Equipment> matchingEquipment = new List<Equipment>();
            foreach (Equipment e in this._equipment)
            {
                if (e.Name.Contains(phrase)) matchingEquipment.Add(e);
            }
            return matchingEquipment;
        }

        public Dictionary<Equipment, List<Room>> FilterByRoomType(RoomType type)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = new Dictionary<Equipment, List<Room>>();

            foreach (Equipment e in _equipment)
            {
                foreach (Room r in e.ArrangmentByRooms.Keys)
                {
                    if (r.Type == type) filteredEquipment[e].Add(r);
                }
            }
            return filteredEquipment;
        }

        public Dictionary<Equipment, List<Room>> FilterByEquipmentType(Dictionary<Equipment, List<Room>> allEquipment, EquipmentType type)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = new Dictionary<Equipment, List<Room>>();

            foreach (Equipment e in this._equipment)
            {
                if (e.Type == type) filteredEquipment[e] = allEquipment[e];
            }
            return filteredEquipment;
        }

        public Dictionary<Equipment, List<Room>> FilterByQuantity(Dictionary<Equipment, List<Room>> allEquipment, int minQuantity, int maxQuantity)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = new Dictionary<Equipment, List<Room>>();
            foreach (Equipment e in allEquipment.Keys)
            {
                foreach (Room r in allEquipment[e])
                {
                    if (e.ArrangmentByRooms[r] >= minQuantity && e.ArrangmentByRooms[r] <= maxQuantity) filteredEquipment[e].Add(r);
                }
            }
            return filteredEquipment;
        }

        public Dictionary<Equipment, List<Room>> filterEquipment(RoomType roomType, int minQuantity, int maxQuantity, EquipmentType equipmentType)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = this.FilterByRoomType(roomType);

            filteredEquipment = this.FilterByQuantity(filteredEquipment, minQuantity, maxQuantity);

            filteredEquipment = this.FilterByEquipmentType(filteredEquipment, equipmentType);

            return filteredEquipment;
        }
    }
}