using HealthInstitution.MVVM.Models.Enumerations;
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

        public int ID { get => this._id; set { this._id = value; } }
        public string Name { get => this._name; set { this._name = value; } }
        public int Number { get => this._number; set { this._number = value; } }
        public RoomType Type { get => this._type; set { this._type = value; } }

        [JsonIgnore]
        public Dictionary<Equipment, int> Equipment { get => this._equipment; set { this._equipment = value; } }
        
        public Room()
        {
            this.Equipment = new Dictionary<Equipment, int>();
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

        public static bool CheckNumber(List<Room> rooms, int number)
        {
            foreach (Room r in rooms)
            {
                if (r.Number == number) return false;
            }
            return true;
        }

        public void ChangeNumber(List<Room> rooms, int newNumber)
        {
            if (CheckNumber(rooms, newNumber)) this.Number = newNumber;
            //Add RoomNumberAlreadyInUseException
            else throw new Exception();
        }

        //private bool CheckType<T>(List<T> schedule)
        //{
        //    foreach (T t in schedule)
        //    {
        //        if (t.ID == this.ID) return false;
        //    }
        //    return true;
        //}

        public void ChangeType(List<Operation> operations, List<Appointment> exams, RoomType newType)
        {
            if (this.Type == RoomType.EXAM_ROOM)
            {
                //Check exams
            } else if (this.Type == RoomType.OPERATING_ROOM)
            {
                //Check operations
            }
            this.Type = newType;
        }

        public bool DeletionCheck(List<Operation> operations, List<Appointment> appointments)
        {
            if (this.Type == RoomType.OPERATING_ROOM)
            {
                //Check exams
            } else if (this.Type == RoomType.EXAM_ROOM)
            {
                //Check operations
            }
            return true;
        }


        public static Room GetById(List<Room> rooms, int id)
        {
            foreach (Room r in rooms)
            {
                if (r.ID == id) return r;
            }
            return null;
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

        public static List<Room> FilterByRoomType(List<Room> allRooms, RoomType type)
        {
            List<Room> filteredRooms = new List<Room>();
            foreach (Room r in allRooms)
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }

        public override string ToString()
        {
            return "Id: " + this.ID + "; Number: " + this.Number + "; Name: " + this.Name + "; Tip: " + this.Type;
        }
    }
}
