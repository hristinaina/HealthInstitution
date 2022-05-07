using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Renovation
    {
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private List<Room> _rooms;
        private List<Room> _result;
        private bool _started;

        public int ID { get => _id; set => _id = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public bool Started { get => _started; set => _started = value; }

        [JsonIgnore]
        public List<Room> Rooms { get => _rooms; set => _rooms = value; }
        [JsonIgnore]
        public List<Room> Result { get => _result; set => _result = value; }

        public Renovation()
        {
            _rooms = new List<Room>();
            _result = new List<Room>();
        }
        public Renovation(int id, DateTime startDate, DateTime endDate, List<Room> rooms, List<Room> result)
        {
            _startDate = startDate;
            _endDate = endDate;
            _rooms = rooms;
            _result = result;
            _id = id;
        }

        public void StartRenovation()
        {
            foreach (Room r in _rooms) r.UnderRenovation = true;
            _started = true;
        }

        public void EndRenovation()
        {
            if (_rooms.Count() > 1)
            {
                //room is deleted
                foreach (Room r in _rooms) Institution.Instance().RoomRepository.Rooms.Remove(r);
            } else if (_result.Count() > 1)
            {
                Institution.Instance().RoomRepository.Rooms.Remove(_rooms[0]);

                foreach (Room r in _result) Institution.Instance().RoomRepository.Rooms.Add(r);
            } else
            {
                _rooms[0].UnderRenovation = false;
            }
        }
    }
}
