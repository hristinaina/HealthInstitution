using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services.Rooms;

namespace HealthInstitution.Core.Repositories
{
    public class RoomRepository
    {
        private readonly string _roomsfileName;
        private readonly string _deletedRoomsfileName;
        private readonly string _futureRoomsfileName;

        private List<Room> _rooms;
        private List<Room> _deletedRooms;
        private List<Room> _futureRooms;

        public List<Room> Rooms { get => _rooms; }
        public List<Room> DeletedRooms { get => _deletedRooms; set => _deletedRooms = value; }
        public List<Room> FutureRooms { get => _futureRooms; set => _futureRooms = value; }

        public RoomRepository(string roomsFileName, string futureRoomsfileName, string deletedRoomsfileName)
        {
            _roomsfileName = roomsFileName;
            _futureRoomsfileName = futureRoomsfileName;
            _deletedRoomsfileName = deletedRoomsfileName;
            _rooms = new List<Room>();
        }

        public void LoadFromFile()
        {
            _rooms = FileService.Deserialize<Room>(_roomsfileName);
            _deletedRooms = FileService.Deserialize<Room>(_deletedRoomsfileName);
            _futureRooms = FileService.Deserialize<Room>(_futureRoomsfileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Room>(_roomsfileName, _rooms);
            FileService.Serialize<Room>(_deletedRoomsfileName, _deletedRooms);
            FileService.Serialize<Room>(_futureRoomsfileName, _futureRooms);
        }

        public Room FindById(int id)
        {
            foreach (Room r in _rooms)
            {
                if (r.ID == id) return r;
            }

            foreach (Room r in _futureRooms)
            {
                if (r.ID == id) return r;
            }
            
            foreach (Room r in _deletedRooms)
            {
                if (r.ID == id) return r;
            }
            
            return null;
        }

        public bool CheckNumber(int number, List<int> ignore)
        {
            foreach (Room r in _rooms)
            {
                if (r.Number == number && !ignore.Contains(number)) return false;
            }

            return true;
        }

        private bool CheckID(int id)
        {
            foreach (Room r in _rooms)
            {
                if (r.ID == id) return false;
            }
            
            foreach (Room r in _futureRooms)
            {
                if (r.ID == id) return false;
            }
            
            foreach (Room r in _deletedRooms)
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

        public Room AddRoom(Room room, bool future = false, List<int> ignoredNumbers = null)
        {
            if (room.Number == 0)
            {
                throw new ZeroRoomNumberException("Room number cannot be zero");
            }
            else if (room.Name is null || room.Name.Equals(""))
            {
                throw new EmptyNameException("Room name cannot be empty");
            } else if (!CheckNumber(room.Number, ignoredNumbers))
            {
                throw new RoomNumberAlreadyTakenException("Room number already taken");
            }

            room.ID = GetID();

            if (!future) 
            { 
                _rooms.Add(room);
            } else
            {
                _futureRooms.Add(room);
            }

            return room;
        }

        public void DeleteRoom(Room r)
        {
            RoomService room = new RoomService(r);
            if (!room.IsChangeable()) throw new RoomCannotBeChangedException("Room cannot be deleted, because it has scheduled appointments");
            room.ReturnEquipmentToWarehouse(DateTime.Today);
            _rooms.Remove(r);
            _deletedRooms.Add(r);
        }
    }
}
