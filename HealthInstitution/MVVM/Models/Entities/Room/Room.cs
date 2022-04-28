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

        public int ID { get => this._id; set { this._id = value; } }
        public string Name { get => this._name; set { this._name = value; } }
        public int Number { get => this._number; set { this._number = value; } }
        public RoomType Type { get => this._type; set { this._type = value; } }

        [JsonIgnore]
        public Dictionary<Equipment, int> Equipment { get => this._equipment; set { this._equipment = value; } }

        [JsonIgnore]
        public List<Appointment> Appointments { get => this._appointments; set { this._appointments = value; } }

        public Room()
        {
            this.Equipment = new Dictionary<Equipment, int>();
            this.Appointments = new List<Appointment>();
        }

        private Room(int id, int number, string name, RoomType type) : this()
        {
            this._id = id;
            this._number = number;
            this._name = name;
            this._type = type;
        }

        public static Room Create(int id, int number, string name, RoomType type)
        {
            return new Room(id, number, name, type);
        }

        private static bool CheckID(List<Room> rooms, int id)
        {
            foreach (Room r in rooms)
            {
                if (r.ID == id) return false;
            }
            return true;
        }

        public static int GetID(List<Room> rooms)
        {
            int i = 1;
            while (true)
            {
                if (CheckID(rooms, i)) return i;
                i++;
            }
        }


        public void ChangeNumber(RoomRepository repository, int newNumber)
        {
            if (repository.CheckNumber(newNumber)) this.Number = newNumber;
            //Add RoomNumberAlreadyInUseException
            else throw new Exception();
        }

        private bool IsChangeble()
        {
            return (this._appointments == null || this._appointments.Count == 0);
        }

        public void ChangeType(OperationRepository operations, ExaminationRepository examinations, RoomType newType)
        {
            bool availableForChange = true;
            if (this.Type == RoomType.EXAM_ROOM || this.Type == RoomType.OPERATING_ROOM)
            {
                availableForChange = this.IsChangeble();
            }
            if (availableForChange)
            {
                this.Type = newType;
            } else
            {
                //throw cannotChangeException
            }
        }

        public bool IsDeletable()
        {
            return this.IsChangeble();
        }

        public void AddEquipment(Equipment e, int quantity)
        {
            if (!this.Equipment.ContainsKey(e))
            {
                this.Equipment[e] = 0;
            }
            this.Equipment[e] += quantity;
        }

        public void RemoveEquipment(Equipment e, int quantity)
        {
            if (!this.Equipment.ContainsKey(e))
            {
                //add NotInRoomException
            }
            this.Equipment[e] -= quantity;
        }

        public override string ToString()
        {
            return "Id: " + this.ID + "; Number: " + this.Number + "; Name: " + this.Name + "; Tip: " + this.Type;
        }
    }
}
