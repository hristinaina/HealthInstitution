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
        private readonly string _fileName;
        private List<Entities.Room> _rooms;

        public List<Entities.Room> Rooms {get => _rooms;}

        public RoomRepository(string roomsFileName)
        {
            _fileName = roomsFileName;
            _rooms = new List<Entities.Room>();
        }

        public void LoadFromFile()
        {
            _rooms = FileService.Deserialize<Entities.Room>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Entities.Room>(_fileName, _rooms);
        }

        public Entities.Room FindById(int id)
        {
            foreach (Entities.Room r in _rooms)
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

        public List<Entities.Room> FilterByRoomType(RoomType type)
        {
            List<Entities.Room> filteredRooms = new();
            foreach (Entities.Room r in _rooms)
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }
    }
}
