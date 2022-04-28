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

        public int ID { get => _id; set => _id = value; }
        public string Name { get => this._name; set => _name = value; }
        public int Quantity { get => this._quantity; set => _quantity = value; }
        public EquipmentType Type { get => _type; set => _type = value; }
        [JsonIgnore]
        public Dictionary<Room, int> ArrangmentByRooms
        {
            get
            {
                if (_arrangmentByRooms is null) _arrangmentByRooms = new Dictionary<Room, int>();
                return _arrangmentByRooms;
            }
            set
            {
                _arrangmentByRooms = value;
            }
        }

        public Equipment()
        {

        }

        public Equipment(int id, string name, int quantity, EquipmentType type)
        {
            _id = id;
            _name = name;
            _quantity = quantity;
            _type = type;
        }

        public Equipment(int id, string name, int quantity, EquipmentType type, Dictionary<Room, int> arragment) : this(id, name, quantity, type)
        {
            _arrangmentByRooms = arragment;
        }

        public void ArrangeInRoom(Room r, int quantity)
        {
            if (!_arrangmentByRooms.ContainsKey(r))
            {
                _arrangmentByRooms[r] = 0;
            }
            _arrangmentByRooms[r] += quantity;
        }

        public int GetQuantityInRoom(Room r)
        {
            return _arrangmentByRooms[r];
        }

    }
}
