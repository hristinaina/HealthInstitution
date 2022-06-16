using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.Rooms;

namespace HealthInstitution.Core.Repositories
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        private readonly string _deletedRoomsFileName;
        private readonly string _futureRoomsFileName;

        private List<Room> _rooms;
        private List<Room> _deletedRooms;
        private List<Room> _futureRooms;

        public List<Room> Rooms { get => _rooms; }
        public List<Room> DeletedRooms { get => _deletedRooms; set => _deletedRooms = value; }
        public List<Room> FutureRooms { get => _futureRooms; set => _futureRooms = value; }

        public RoomRepository(string roomsFileName, string futureRoomsFileName, string deletedRoomsFileName)
        {
            _fileName = roomsFileName;
            _futureRoomsFileName = futureRoomsFileName;
            _deletedRoomsFileName = deletedRoomsFileName;
            _rooms = new List<Room>();
        }

        public override void LoadFromFile()
        {
            _rooms = FileService.Deserialize<Room>(_fileName);
            _deletedRooms = FileService.Deserialize<Room>(_deletedRoomsFileName);
            _futureRooms = FileService.Deserialize<Room>(_futureRoomsFileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<Room>(_fileName, _rooms);
            FileService.Serialize<Room>(_deletedRoomsFileName, _deletedRooms);
            FileService.Serialize<Room>(_futureRoomsFileName, _futureRooms);
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

        public int GetNewID()
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

            room.ID = GetNewID();

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
            RoomService service = new RoomService();
            if (!service.IsChangeable(r)) throw new RoomCannotBeChangedException("Room cannot be deleted, because it has scheduled appointments");
            service.ReturnEquipmentToWarehouse(DateTime.Today, r);
            _rooms.Remove(r);
            _deletedRooms.Add(r);
        }

        public List<Room> GetCurrentRooms()
        {
            return _rooms;
        }

        public List<Room> GetDeletedRooms()
        {
            return _deletedRooms;
        }

        public List<Room> GetFutureRooms()
        {
            return _futureRooms;
        }
    }
}
