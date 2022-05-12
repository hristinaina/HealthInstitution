using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class RoomRepository
    {
        private readonly string _roomsfileName;
        private readonly string _deletedRoomsfileName;
        private readonly string _futureRoomsfileName;

        private List<Entities.Room> _rooms;
        private List<Entities.Room> _deletedRooms;
        private List<Entities.Room> _futureRooms;

        public List<Entities.Room> Rooms { get => _rooms; }
        public List<Entities.Room> DeletedRooms { get => _deletedRooms; set => _deletedRooms = value; }
        public List<Entities.Room> FutureRooms { get => _futureRooms; set => _futureRooms = value; }

        public RoomRepository(string roomsFileName, string futureRoomsfileName, string deletedRoomsfileName)
        {
            _roomsfileName = roomsFileName;
            _futureRoomsfileName = futureRoomsfileName;
            _deletedRoomsfileName = deletedRoomsfileName;
            _rooms = new List<Entities.Room>();
        }

        public void LoadFromFile()
        {
            _rooms = FileService.Deserialize<Entities.Room>(_roomsfileName);
            _deletedRooms = FileService.Deserialize<Entities.Room>(_deletedRoomsfileName);
            _futureRooms = FileService.Deserialize<Entities.Room>(_futureRoomsfileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Entities.Room>(_roomsfileName, _rooms);
            FileService.Serialize<Entities.Room>(_deletedRoomsfileName, _deletedRooms);
            FileService.Serialize<Entities.Room>(_futureRoomsfileName, _futureRooms);
        }

        public Entities.Room FindById(int id)
        {
            foreach (Entities.Room r in _rooms)
            {
                if (r.ID == id) return r;
            }

            foreach (Entities.Room r in _futureRooms)
            {
                if (r.ID == id) return r;
            }
            
            foreach (Entities.Room r in _deletedRooms)
            {
                if (r.ID == id) return r;
            }
            
            return null;
        }

        public bool CheckNumber(int number)
        {
            foreach (Entities.Room r in _rooms)
            {
                if (r.Number == number) return false;
            }

            return true;
        }

        private bool CheckID(int id)
        {
            foreach (Entities.Room r in _rooms)
            {
                if (r.ID == id) return false;
            }
            
            foreach (Entities.Room r in _futureRooms)
            {
                if (r.ID == id) return false;
            }
            
            foreach (Entities.Room r in _deletedRooms)
            {
                if (r.ID == id) return false;
            }
            
            return true;
        }

        public int GetID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

        public void CreateRoom(int id, string name, int number, RoomType type)
        {
            Entities.Room r = new Entities.Room(id, number, name, type);
            _rooms.Add(r);
        }

        public List<Entities.Room> FilterByRoomType(RoomType type)
        {
            List<Entities.Room> filteredRooms = new();
            foreach (Entities.Room r in _rooms)
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }

        public void FindAvailableRoom(Appointment a, DateTime wantedTime)
        {
            RoomType type = RoomType.EXAM_ROOM;
            bool changing = false;
            if (a is Operation) type = RoomType.OPERATING_ROOM; 

            if (a.Room != null)
            {
                changing = true;
                if (a.Room.isAvailable(wantedTime, a))
                {
                    a.Date = wantedTime;
                    return;
                }
                else a.Room.Appointments.Remove(a);
            }

            List<Entities.Room> rooms = FilterByRoomType(type);
            foreach (Entities.Room r in rooms)
            {
                if(r.isAvailable(wantedTime, a))
                {
                    r.Appointments.Add(a);
                    a.Room = r;
                    if (changing) a.Date = wantedTime;
                    return;
                }
            }
        }
    }
}
