using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services.Equipments;

namespace HealthInstitution.Core.Services.Rooms
{
    public class RoomService
    {

        public RoomService()
        {
        }

        public bool IsChangeable(Room room)
        {
            foreach (Appointment a in room.Appointments)
            {
                if (a.Date >= DateTime.Today) return false;
            }
            return true;
        }

        public void AddEquipment(Equipment e, int quantity, Room room)
        {
            if (!room.Equipment.ContainsKey(e))
            {
                room.Equipment[e] = 0;
            }
            room.Equipment[e] += quantity;
        }

        public void ReturnEquipmentToWarehouse(DateTime date, Room room)
        {
            EquipmentService equipmentService = new EquipmentService();
            foreach (Equipment e in room.Equipment.Keys)
            {
                equipmentService.ReturnToWarehouse(date, room, e);
            }
        }
        public bool isAvailable(DateTime appointmentTime, Appointment appointment, Room room)
        {
            if (room.UnderRenovation) return false;

            bool free = true;
            foreach (Appointment a in room.Appointments)
            {
                if (a.Date.Date == appointmentTime.Date && a.ID != appointment.ID)
                {
                    if (!(a.Date.TimeOfDay + TimeSpan.FromMinutes(15) <= appointmentTime.TimeOfDay || a.Date.TimeOfDay - TimeSpan.FromMinutes(15) >= appointmentTime.TimeOfDay))
                    {
                        free = false;
                        break;
                    }
                }
            }
            return free;
        }

        public bool IsUnderRenovation(DateTime startDate, DateTime endDate, Room room)
        {
            foreach (Renovation r in room.Renovations)
            {
                if ((r.StartDate < endDate && r.StartDate > startDate) || (r.EndDate < endDate && r.EndDate > startDate))
                {
                    return true;
                }
            }
            return false;
        }

        public void Change(string newName, int newNumber, RoomType newType, Room room)
        {
            if (newName is null || newName.Equals("")) throw new EmptyNameException("Room name cannot be empty");
            else if (newNumber == 0) throw new ZeroRoomNumberException("Room number cannot be 0");
            else if (!Institution.Instance().RoomRepository.CheckNumber(newNumber, new List<int> { room.Number })) throw new RoomNumberAlreadyTakenException("Room number already taken");
            else if (newType != room.Type && !IsChangeable()) throw new RoomCannotBeChangedException("Room cannot be changed, because it has scheduled appointments");
            room.Name = newName;
            room.Number = newNumber;
            room.Type = newType;
        }
    }
}