using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;
using HealthInstitution.Exceptions.AdminExceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Services.Equipments;

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

        public void ReturnEquipmentToWarehouse(DateTime date)
        {
            foreach (Equipment e in _equipment.Keys)
            {
                EquipmentService equipment = new EquipmentService(e);
                equipment.ReturnToWarehouse(date, this);
            }
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

        public bool IsUnderRenovation(DateTime startDate, DateTime endDate)
        {
            foreach (Renovation r in _renovations)
            {
                if ((r.StartDate < endDate && r.StartDate > startDate) || (r.EndDate < endDate && r.EndDate > startDate))
                {
                    return true;
                }
            }
            return false;
        }

        public void Change(string newName, int newNumber, RoomType newType)
        {
            if (newName is null || newName.Equals("")) throw new EmptyRoomNameException("Room name cannot be empty");
            else if (newNumber == 0) throw new ZeroRoomNumberException("Room number cannot be 0");
            else if (!Institution.Instance().RoomRepository.CheckNumber(newNumber, new List<int> { _number })) throw new RoomNumberAlreadyTakenException("Room number already taken");
            else if (newType != _type && !IsChangeable()) throw new RoomCannotBeChangedException("Room cannot be changed, because it has scheduled appointments");
            _name = newName;
            _number = newNumber;
            _type = newType;
        }

        public override string ToString()
        {
            return _name;
        }

    }
}
