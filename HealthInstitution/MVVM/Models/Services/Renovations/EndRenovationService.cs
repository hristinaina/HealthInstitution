using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services.Renovations
{
    class EndRenovationService
    {
        private Renovation _renovation;
        private RoomRepository _repository;

        public EndRenovationService(Renovation r)
        {
            _renovation = r;
            _repository = Institution.Instance().RoomRepository;
        }

        private void MergeRooms()
        {
            Room resultingRoom = _renovation.Result[0];

            //rooms are deleted
            foreach (Room r in _renovation.RoomsUnderRenovation)
            {
                _repository.Rooms.Remove(r);
                _repository.DeletedRooms.Add(r);
            }

            _repository.FutureRooms.Remove(resultingRoom);
            _repository.Rooms.Add(resultingRoom);
        }

        private void DivideRooms()
        {
            Room roomUnderRenovation = _renovation.RoomsUnderRenovation[0];
            _repository.Rooms.Remove(roomUnderRenovation);
            _repository.DeletedRooms.Add(roomUnderRenovation);

            foreach (Room r in _renovation.Result)
            {
                _repository.FutureRooms.Remove(r);
                _repository.Rooms.Add(r);
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
