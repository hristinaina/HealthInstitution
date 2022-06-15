using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;
using HealthInstitution.Core.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Services.Equipments;

namespace HealthInstitution.Core
{
    public class Room
    {
        private int _id;
        private string _name;
        private int _number;
        private RoomType? _type;
        private Dictionary<Equipment, int> _equipment;
        private List<Appointment> _appointments;
        private List<Renovation> _renovations;
        private bool _underRenovation;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Number { get => _number; set => _number = value;  }
        public RoomType? Type { get => _type; set => _type = value;  }
        public bool UnderRenovation { get => _underRenovation; set => _underRenovation = value; }

        [JsonIgnore]
        public Dictionary<Equipment, int> Equipment { get => _equipment; set => _equipment = value;  }

        [JsonIgnore]
        public List<Appointment> Appointments { get => _appointments; set => _appointments = value; }
        [JsonIgnore]
        public List<Renovation> Renovations { get => _renovations; set => _renovations = value; }

        public Room()
        {
            _equipment = new Dictionary<Equipment, int>();
            _appointments = new List<Appointment>();
            _renovations = new List<Renovation>();
        }

        public Room(string name, int number, RoomType type) : this()
        {
            _number = number;
            _name = name;
            _type = type;
        }

        public Room(int id, string name, int number, RoomType type) : this()
        {
            _id = id;
            _number = number;
            _name = name;
            _type = type;
        }

        public override string ToString()
        {
            return _name;
        }

    }
}
