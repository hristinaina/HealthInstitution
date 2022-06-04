using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models.Services.Rooms;

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

        public bool CheckNumber(int number, List<int> ignore)
        {
            foreach (Entities.Room r in _rooms)
            {
                if (r.Number == number && !ignore.Contains(number)) return false;
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

        public Entities.Room AddRoom(Entities.Room room, bool future = false, List<int> ignoredNumbers = null)
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

        public void DeleteRoom(Entities.Room r)
        {
            RoomService room = new RoomService(r);
            if (!room.IsChangeable()) throw new RoomCannotBeChangedException("Room cannot be deleted, because it has scheduled appointments");
            room.ReturnEquipmentToWarehouse(DateTime.Today);
            _rooms.Remove(r);
            _deletedRooms.Add(r);
        }
    }
}
