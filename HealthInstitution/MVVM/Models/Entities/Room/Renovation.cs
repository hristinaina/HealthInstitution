using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities.Room
{
    public class Renovation
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private List<Room> _rooms;
        private List<Room> _result;

        public DateTime EndDate { get => _endDate; set => _endDate = value; }
        public DateTime StartDate { get => _startDate; set => _startDate = value; }

        [JsonIgnore]
        public List<Room> Rooms { get => _rooms; set => _rooms = value; }
        [JsonIgnore]
        public List<Room> Result { get => _result; set => _result = value; }

        public Renovation(DateTime startDate, DateTime endDate, List<Room> rooms, List<Room> result)
        {
            _startDate = startDate;
            _endDate = endDate;
            _rooms = rooms;
            _result = result;
        }

        public void EndRenovaton()
        {
            //make results
        }
    }
}
