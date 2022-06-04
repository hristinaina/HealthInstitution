using System;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services.Rooms
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
            if (a is Operation) type = RoomType.OPERATING_ROOM;

            if (a.Room != null)
            {
                changing = true;
                RoomService room = new RoomService(a.Room);
                if (room.isAvailable(wantedTime, a))
                {
                    a.Date = wantedTime;
                    return;
                }
                else a.Room.Appointments.Remove(a);
            }

            FilterRoomService service = new FilterRoomService();
            List<Entities.Room> rooms = service.FilterByRoomType(type);
            foreach (Entities.Room r in rooms)
            {
                RoomService room = new RoomService(r);
                if (room.isAvailable(wantedTime, a))
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