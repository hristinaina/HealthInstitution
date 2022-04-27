using HealthInstitution.MVVM.Models.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Equipment
    {
        private int _id;
        private string _name;
        private int _quantity;
        private EquipmentType _type;
        private Dictionary<Room, int> _arrangmentByRooms;

        public int ID { get => _id; }
        public string Name { get => this._name; set { this._name = value; } }
        public int Quantity { get => this._quantity; set { this._quantity = value; } }
        public EquipmentType Type { get => this._type; set { this._type = value; } }
        [JsonIgnore]
        public Dictionary<Room, int> ArrangmentByRooms {
            get 
            {
                if (this._arrangmentByRooms == null) this._arrangmentByRooms = new Dictionary<Room, int>();
                return this._arrangmentByRooms;
            }
            set
            {
                this._arrangmentByRooms = value;
            }
                }

        public Equipment()
        {

        }

        public Equipment(int id, string name, int quantity, EquipmentType type)
        {
            this._id = id;
            this._name = name;
            this._quantity = quantity;
            this._type = type;
        }

        public Equipment(int id, string name, int quantity, EquipmentType type, Dictionary<Room, int> arragment) : this(id, name, quantity, type)
        {
            this._arrangmentByRooms = arragment;
        }

        public void ArrangeInRoom(Room r, int quantity)
        {
            if (!this._arrangmentByRooms.ContainsKey(r))
            {
                this._arrangmentByRooms[r] = 0;
            }
            this._arrangmentByRooms[r] += quantity;
        }

        public static Equipment GetById(List<Equipment> equipment, int id)
        {
            foreach (Equipment e in equipment)
            {
                if (e.ID == id) return e;
            }
            return null;
        }

        public static List<Equipment> Search(List<Equipment> equipment, string phrase)
        {
            List<Equipment> matchingEquipment = new List<Equipment>();
            foreach (Equipment e in equipment)
            {
                if (e.Name.Contains(phrase)) matchingEquipment.Add(e);
            }
            return matchingEquipment;
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

        //public static List<Equipment> FilterByRoomType(List<Equipment> allEquipment, RoomType type)
        //{
        //    return new List<Equipment>();
        //}

        public static List<Equipment> FilterByEquipmentType(List<Equipment> allEquipment, EquipmentType type)
        {
            List<Equipment> filteredEquipment = new List<Equipment>();

            foreach (Equipment e in allEquipment)
            {
                if (e.Type == type) filteredEquipment.Add(e);
            }
            return filteredEquipment;
        }
    }
}
