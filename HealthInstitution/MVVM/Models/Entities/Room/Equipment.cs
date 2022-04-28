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

        public int ID { get => _id; set { this._id = value; } }
        public string Name { get => this._name; set { this._name = value; } }
        public int Quantity { get => this._quantity; set { this._quantity = value; } }
        public EquipmentType Type { get => this._type; set { this._type = value; } }
        [JsonIgnore]
        public Dictionary<Room, int> ArrangmentByRooms
        {
            get
            {
                if (this._arrangmentByRooms is null) this._arrangmentByRooms = new Dictionary<Room, int>();
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

        public int GetQuantityInRoom(Room r)
        {
            return this._arrangmentByRooms[r];
        }

    }
}
