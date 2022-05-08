using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
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

        public Room(int id, int number, string name, RoomType type) : this()
        {
            _id = id;
            _number = number;
            _name = name;
            _type = type;
        }

        public bool IsChangeable()
        {
            foreach (Appointment a in _appointments)
            {
                if (a.Date >= DateTime.Today) return false;
            }
            return true;
        }

        public void AddEquipment(Equipment e, int quantity)
        {
            if (!_equipment.ContainsKey(e))
            {
                _equipment[e] = 0;
            }
            _equipment[e] += quantity;
        }

        public bool isAvailable(DateTime appointmentTime, Appointment appointment)
        {
            if (_underRenovation) return false;

            bool free = true;
            foreach (Appointment a in _appointments)
            {
                if (a.Date.Date == appointmentTime.Date && a.ID != appointment.ID)
                {
                    if (!(a.Date.TimeOfDay + TimeSpan.FromMinutes(15) <= appointmentTime.TimeOfDay || a.Date.TimeOfDay - TimeSpan.FromMinutes(15) >= appointmentTime.TimeOfDay))
                    {
                        free = false;
                        break;
                    }
                }
            }
            return free;
        }

        public override string ToString()
        {
            return _name;
        }

    }
}
