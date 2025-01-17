﻿using HealthInstitution.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core
{
    public class Equipment
    {
        private int _id;
        private string _name;
        private int _quantity;
        private EquipmentType _type;
        private Dictionary<Room, int> _arrangmentByRooms;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
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
            _arrangmentByRooms = new Dictionary<Room, int>();
        }

        // constructor copy
        public Equipment(Equipment equipment)
        {
            _id = equipment.ID;
            _name = equipment.Name;
            _quantity = equipment.Quantity;
            _type = equipment.Type;
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

        public override string ToString()
        {
            return _name;

        }
    }
}
