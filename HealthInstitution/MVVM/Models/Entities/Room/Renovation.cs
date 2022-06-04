using HealthInstitution.Exceptions.AdminExceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Services.Rooms;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Renovation
    {
        private int _id;
        private DateTime _startDate;
        private DateTime _endDate;
        private List<Room> _roomsUnderRenovation;
        private List<Room> _result;
        private bool _started;

        public int ID { get => _id; set => _id = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public bool Started { get => _started; set => _started = value; }

        [JsonIgnore]
        public List<Room> RoomsUnderRenovation { get => _roomsUnderRenovation;
            set
            {
                foreach (Entities.Room r in value)
                {
                    RoomService room = new RoomService(r);
                    if (room.IsUnderRenovation(_startDate, _endDate)) throw new RoomUnderRenovationException("Room already under renovation at that time");
                }
                _roomsUnderRenovation = value;
            }
        }
        [JsonIgnore]
        public List<Room> Result { get => _result; set => _roomsUnderRenovation = value; }

        public Renovation()
        {
            _roomsUnderRenovation = new List<Room>();
            _result = new List<Room>();
        }

        public Renovation(int id, DateTime startDate, DateTime endDate) : this()
        {
            _startDate = startDate;
            _endDate = endDate;
            _id = id;
        }

        public Renovation(int id, DateTime startDate, DateTime endDate, List<Room> rooms, List<Room> result) : this()
        {
            _startDate = startDate;
            _endDate = endDate;
            _roomsUnderRenovation = rooms;
            _result = result;
            _id = id;
        }

        public bool IsStarted()
        {
            return _startDate <= DateTime.Today && !_started;
        }
    }
}
