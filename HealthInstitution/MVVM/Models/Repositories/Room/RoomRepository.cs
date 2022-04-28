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
        private string _fileName;
        private List<Entities.Room> _rooms;

        public List<Entities.Room> Rooms {get => this._rooms;}

        public RoomRepository(string roomsFileName)
        {
            this._fileName = roomsFileName;
            this._rooms = new List<Entities.Room>();
        }

        public void LoadFromFile()
        {
            this._rooms = FileService.Deserialize<Entities.Room>(this._fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Entities.Room>(this._fileName, this._rooms);
        }

        public Entities.Room FindById(int id)
        {
            foreach (Entities.Room r in this._rooms)
            {
                if (r.ID == id) return r;
            }
            return null;
        }

        public bool CheckNumber(int number)
        {
            foreach (Entities.Room r in this._rooms)
            {
                if (r.Number == number) return false;
            }
            return true;
        }

        public List<Entities.Room> FilterByRoomType(RoomType type)
        {
            List<Entities.Room> filteredRooms = new List<Entities.Room>();
            foreach (Entities.Room r in this._rooms)
            {
                if (r.Type == type) filteredRooms.Add(r);
            }
            return filteredRooms;
        }
    }
}
