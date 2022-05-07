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

        public int ID { get => _id; set => _id = value; }
        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }

        [JsonIgnore]
        public List<Room> Rooms { get => _rooms; set => _rooms = value; }
        [JsonIgnore]
        public List<Room> Result { get => _result; set => _result = value; }

        public Renovation()
        {

        }
        public Renovation(int id, DateTime startDate, DateTime endDate, List<Room> rooms, List<Room> result)
        {
            _startDate = startDate;
            _endDate = endDate;
            _rooms = rooms;
            _result = result;
            _id = id;
        }

        public void EndRenovaton()
        {
            //make results
        }
    }
}
