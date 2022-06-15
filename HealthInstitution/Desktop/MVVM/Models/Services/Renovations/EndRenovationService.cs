using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;

namespace HealthInstitution.Core.Services.Renovations
{
    class EndRenovationService
    {
        private Renovation _renovation;
        private RoomRepository _rooms;

        public EndRenovationService(Renovation r)
        {
            _renovation = r;
            _rooms = Institution.Instance().RoomRepository;
        }

        private void MergeRooms()
        {
            Room resultingRoom = _renovation.Result[0];

            //rooms are deleted
            foreach (Room r in _renovation.RoomsUnderRenovation)
            {
                _rooms.Rooms.Remove(r);
                _rooms.DeletedRooms.Add(r);
            }

            _rooms.FutureRooms.Remove(resultingRoom);
            _rooms.Rooms.Add(resultingRoom);
        }

        private void DivideRooms()
        {
            Room roomUnderRenovation = _renovation.RoomsUnderRenovation[0];
            _rooms.Rooms.Remove(roomUnderRenovation);
            _rooms.DeletedRooms.Add(roomUnderRenovation);

            foreach (Room r in _renovation.Result)
            {
                _rooms.FutureRooms.Remove(r);
                _rooms.Rooms.Add(r);
            }
        }

        public void EndRenovation()
        {
            if (_renovation.RoomsUnderRenovation.Count() > 1)
            {
                MergeRooms();
            }
            else if (_renovation.Result.Count() > 1)
            {
                DivideRooms();
            }
            else
            {
                if (_renovation.RoomsUnderRenovation.Count > 0)
                    _renovation.RoomsUnderRenovation[0].UnderRenovation = false;
            }
        }
    }
}
