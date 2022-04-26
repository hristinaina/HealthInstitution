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

        public Equipment(int id, string name, int quantity)
        {
            this.ID = id;
            this.Name = name;
            this.Quantity = quantity;
        }

        public Equipment(int id, string name, int quantity, Dictionary<Room, int> arragment) : this(id, name, quantity)
        {
            this.ArrangmentByRoom = arragment;
        }

        public void ArrangeByRooms(List<Room> rooms, List<EquipmentArragment> arragments)
        {
            foreach (EquipmentArragment a in arragments)
            {
                if (a.EquipmentId == this.ID)
                {
                    this.ArrangmentByRoom[Room.GetById(rooms, a.RoomId)] = a.Quantity;
                }
            }
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

        public static List<Equipment> FilterByQuantity()
        {

        }
    }
}
