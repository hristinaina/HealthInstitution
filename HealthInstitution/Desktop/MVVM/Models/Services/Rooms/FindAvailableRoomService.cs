using System;
using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Repositories;

namespace HealthInstitution.Core.Services.Rooms
{
    public class FindAvailableRoomService
    {
        private RoomRepository _rooms;

        public FindAvailableRoomService()
        {
            _rooms = Institution.Instance().RoomRepository;
        }

        public void FindAvailableRoom(Appointment a, DateTime wantedTime)
        {
            RoomType type = RoomType.EXAM_ROOM;
            bool changing = false;

            RoomService roomService = new RoomService();

            if (a is Operation) type = RoomType.OPERATING_ROOM;

            if (a.Room != null)
            {
                changing = true;
                if (roomService.isAvailable(wantedTime, a, a.Room))
                {
                    a.Date = wantedTime;
                    return;
                }
                else a.Room.Appointments.Remove(a);
            }

            FilterRoomService service = new FilterRoomService();
            List<Room> rooms = service.FilterByRoomType(type);
            foreach (Room r in rooms)
            {
                if (roomService.isAvailable(wantedTime, a, a.Room))
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