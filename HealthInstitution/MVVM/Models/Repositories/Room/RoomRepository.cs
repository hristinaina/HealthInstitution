using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
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
        private string _fileName;
        private List<Room> _rooms;

        public List<Room> Rooms {get => this._rooms;}

        public RoomRepository(string roomsFileName)
        {
            this._fileName = roomsFileName;
            this._rooms = new List<Room>();
        }

        public void LoadFromFile()
        {
            this._rooms = FileService.Deserialize<Room>(this._fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Room>(this._fileName, this._rooms);
        }

        public Room GetById(int id)
        {
            foreach (Room r in this._rooms)
            {
                if (r.ID == id) return r;
            }
            return null;
        }

        public bool CheckNumber(int number)
        {
            foreach (Room r in this._rooms)
            {
                if (r.Number == number) return false;
            }
            return true;
        }

        public List<Room> FilterByRoomType(RoomType type)
        {
            List<Room> filteredRooms = new List<Room>();
            foreach (Room r in this._rooms)
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }
    }
}
