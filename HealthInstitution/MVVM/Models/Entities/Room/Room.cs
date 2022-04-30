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
        private RoomType _type;
        private Dictionary<Equipment, int> _equipment;
        private List<Appointment> _appointments;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Number { get => _number; set => _number = value;  }
        public RoomType Type { get => _type; set => _type = value;  }

        [JsonIgnore]
        public Dictionary<Equipment, int> Equipment { get => _equipment; set => _equipment = value;  }

        [JsonIgnore]
        public List<Appointment> Appointments { get => _appointments; set => _appointments = value; }

        public Room()
        {
            this.Equipment = new Dictionary<Equipment, int>();
            this.Appointments = new List<Appointment>();
        }

        private Room(int id, int number, string name, RoomType type) : this()
        {
            _id = id;
            _number = number;
            _name = name;
            _type = type;
        }

        public Room(string name)
        {
            _name = name;
        }

        public static Room Create(int id, int number, string name, RoomType type)
        {
            return new Room(id, number, name, type);
        }

        public void ChangeNumber(RoomRepository repository, int newNumber)
        {
            if (repository.CheckNumber(newNumber)) Number = newNumber;
            //Add RoomNumberAlreadyInUseException
            else throw new Exception();
        }

        private bool IsChangeble()
        {
            return (_appointments == null || _appointments.Count == 0);
        }

        public void ChangeType(OperationRepository operations, ExaminationRepository examinations, RoomType newType)
        {
            bool availableForChange = true;
            if (_type == RoomType.EXAM_ROOM || _type == RoomType.OPERATING_ROOM)
            {
                availableForChange = this.IsChangeble();
            }
            if (availableForChange)
            {
                _type = newType;
            } else
            {
                //throw cannotChangeException
            }
        }

        public bool IsDeletable()
        {
            return IsChangeble();
        }

        public void AddEquipment(Equipment e, int quantity)
        {
            if (!_equipment.ContainsKey(e))
            {
                _equipment[e] = 0;
            }
            _equipment[e] += quantity;
        }

        public void RemoveEquipment(Equipment e, int quantity)
        {
            if (!_equipment.ContainsKey(e))
            {
                //add NotInRoomException
            }
            _equipment[e] -= quantity;
        }

        public bool isAvailable(DateTime appointmentTime, Appointment appointment)
        {
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
