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

        public Equipment GetById(int id)
        {
            foreach (Equipment e in this._equipment)
            {
                if (e.ID == id) return e;
            }
            return null;
        }

        public List<Equipment> FilterByEquipmentType(EquipmentType type)
        {
            List<Equipment> filteredEquipment = new List<Equipment>();

            foreach (Equipment e in this._equipment)
            {
                if (e.Type == type) filteredEquipment.Add(e);
            }
            return filteredEquipment;
        }

        public static Dictionary<Room, Equipment> FilterByQuantity(Room r, List<Equipment> allEquipment, int minQuantity, int maxQuantity)
        {
            Dictionary<Room, Equipment> filteredEquipment = new Dictionary<Room, Equipment>();
            foreach (Equipment e in allEquipment)
            {
                if (e.ArrangmentByRooms[r] >= minQuantity && e.ArrangmentByRooms[r] <= maxQuantity) filteredEquipment[r] = e;
            }
            return filteredEquipment;
        }
    }
}