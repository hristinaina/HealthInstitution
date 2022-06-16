using System;
using System.Collections.Generic;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    public class FindAvailableRoomService
    {
        private IRoomRepositoryService _rooms;
        private IFilterRoomService _filter;

        public FindAvailableRoomService()
        {
            _rooms = new RoomRepositoryService();
            _filter = new FilterRoomService();
        }

        public void FindAvailableRoom(Appointment a, DateTime wantedTime)
        {
            RoomType type = RoomType.EXAM_ROOM;
            bool changing = false;


            if (a is Operation) type = RoomType.OPERATING_ROOM;

            if (a.Room != null)
            {
                changing = true;
                if (_rooms.isAvailable(wantedTime, a, a.Room))
                {
                    a.Date = wantedTime;
                    return;
                }
                else a.Room.Appointments.Remove(a);
            }

            
            List<Room> rooms = _filter.FilterByRoomType(type);
            foreach (Room r in rooms)
            {
                if (_rooms.isAvailable(wantedTime, a, r))
                {
                    r.Appointments.Add(a);
                    a.Room = r;
                    if (changing) a.Date = wantedTime;
                    return;
                }
            }

            if (a.Room == null)
            {
                throw new Exception("There are no available rooms for this appointment. Please choose another date or time!");
            }
        }
    }
}