using HealthInstitution.MVVM.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Room
    {
        public int ID { get; }
        public string Name { get; set; }
        public int Number { get; set; }
        public RoomType Type { get; set; }
        
        private Room(int id, int number, string name, RoomType type)
        {
            this.ID = id;
            this.Number = number;
            this.Name = name;
            this.Type = type;
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
            //Ovde dodati Exception da se broj vec koristi
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

        public void ChangeType(List<Operation> operations, List<Appointment> appointments, RoomType newType)
        {
            //Ako je operaciona sala, proveri operacije. Ako je sala za reglede, proveri preglede.
            this.Type = newType;
        }

        public bool DeletionCheck(List<Operation> operations, List<Appointment> appointments)
        {
            if (this.Type == RoomType.OPERATING_ROOM)
            {
                //provera da li se nalazi u operacijama
            } else if (this.Type == RoomType.EXAM_ROOM)
            {
                //provera da li se nalazi u pregledima
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
        public override string ToString()
        {
            return "Id: " + this.ID + "; Number: " + this.Number + "; Name: " + this.Name + "; Tip: " + this.Type;
        }
    }
}
