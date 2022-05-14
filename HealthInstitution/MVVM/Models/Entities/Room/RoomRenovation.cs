using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class RoomRenovation
    {
        private int _renovationId;
        private int _roomId;
        private bool _result;

        public int RenovationId { get => _renovationId; set => _renovationId = value; }
        public int RoomId { get => _roomId; set => _roomId = value; }
        public bool Result { get => _result; set => _result = value; }

        public RoomRenovation()
        {
        }

        public RoomRenovation(int renovationId, int roomId, bool result)
        {
            _renovationId = renovationId;
            _roomId = roomId;
            _result = result;
        }
    }
}
