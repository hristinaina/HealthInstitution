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
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public EquipmentType Type { get; set; }
        [JsonIgnore]
        public Dictionary<Room, int> ArrangmentByRoom {
            get 
            {
                if (ArrangmentByRoom == null) ArrangmentByRoom = new Dictionary<Room, int>();
                return ArrangmentByRoom;
            }
            set
            {
                ArrangmentByRoom = value;
            }
                }

        public Equipment()
        {

        }

        public Equipment(int id, string name, int quantity, EquipmentType type)
        {
            this.ID = id;
            this.Name = name;
            this.Quantity = quantity;
            this.Type = type;
        }

        public Equipment(int id, string name, int quantity, EquipmentType type, Dictionary<Room, int> arragment) : this(id, name, quantity, type)
        {
            this.ArrangmentByRoom = arragment;
        }

        public void ArrangeInRoom(Room r, int quantity)
        {
            if (!this.ArrangmentByRoom.ContainsKey(r))
            {
                this.ArrangmentByRoom[r] = 0;
            }
            this.ArrangmentByRoom[r] += quantity;
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
                if (e.ArrangmentByRoom[r] >= minQuantity && e.ArrangmentByRoom[r] <= maxQuantity) filteredEquipment[r] = e;
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
