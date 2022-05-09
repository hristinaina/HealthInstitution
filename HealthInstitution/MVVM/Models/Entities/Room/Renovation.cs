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
        public Renovation(int id, DateTime startDate, DateTime endDate, List<Room> rooms, List<Room> result) : this()
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

            if (_rooms.Count() > 1 || _result.Count() > 1)
            {
                foreach (Room r in _rooms)
                {
                    Dictionary<Equipment, int> equipment = r.Equipment;
                    foreach (Equipment e in equipment.Keys)
                    {
                        Room warehouse = Institution.Instance().RoomRepository.FindById(0);
                        EquipmentArrangement a = Institution.Instance().EquipmentArragmentRepository.FindCurrentArrangement(r, e);
                        a.EndDate = _endDate;
                        Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Remove(a);
                        EquipmentArrangement newArragment = Institution.Instance().EquipmentArragmentRepository.FindCurrentArrangement(warehouse, e);
                        if (newArragment is null)
                        {
                            newArragment = new EquipmentArrangement(e, warehouse, 0, a.EndDate, DateTime.MaxValue);
                            Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Add(newArragment);
                        }
                        newArragment.Quantity += equipment[e];
                    }
                }
                }

        }

        public void EndRenovation()
        {
            if (_rooms.Count() > 1)
            {
                Room resultingRoom = _result[0];

                //room is deleted
                foreach (Room r in _rooms)
                {
                    Institution.Instance().RoomRepository.Rooms.Remove(r);
                    Institution.Instance().RoomRepository.DeletedRooms.Add(r);
                }

                Institution.Instance().RoomRepository.FutureRooms.Remove(resultingRoom);
                Institution.Instance().RoomRepository.Rooms.Add(resultingRoom);
            } else if (_result.Count() > 1)
            {
                Room roomUnderRenovation = _rooms[0];
                Institution.Instance().RoomRepository.Rooms.Remove(roomUnderRenovation);
                Institution.Instance().RoomRepository.DeletedRooms.Add(roomUnderRenovation);

                Dictionary<Equipment, int> equipment = roomUnderRenovation.Equipment;
                foreach (Equipment e in equipment.Keys)
                {
                    EquipmentArrangement a = Institution.Instance().EquipmentArragmentRepository.FindCurrentArrangement(roomUnderRenovation, e);
                    a.EndDate = _endDate;
                    Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Add(new EquipmentArrangement(e, _result[0], equipment[e], a.EndDate, DateTime.MaxValue));
                }
                
                foreach (Room r in _result)
                {
                    Institution.Instance().RoomRepository.FutureRooms.Remove(r);
                    Institution.Instance().RoomRepository.Rooms.Add(r);
                }
            } else
            {
                _rooms[0].UnderRenovation = false;
            }
        }
    }
}
