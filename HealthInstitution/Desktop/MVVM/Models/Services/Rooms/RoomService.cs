using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services.Equipments;

namespace HealthInstitution.Core.Services.Rooms
{
    public class RoomService
    {
        private Room _room;

        public RoomService(Room r)
        {
            _room = r;
        }

        public bool IsChangeable()
        {
            foreach (Appointment a in _room.Appointments)
            {
                if (a.Date >= DateTime.Today) return false;
            }
            return true;
        }

        public void AddEquipment(Equipment e, int quantity)
        {
            if (!_room.Equipment.ContainsKey(e))
            {
                _room.Equipment[e] = 0;
            }
            _room.Equipment[e] += quantity;
        }

        public void ReturnEquipmentToWarehouse(DateTime date)
        {
            foreach (Equipment e in _room.Equipment.Keys)
            {
                EquipmentService equipment = new EquipmentService(e);
                equipment.ReturnToWarehouse(date, _room);
            }
        }
        public bool isAvailable(DateTime appointmentTime, Appointment appointment)
        {
            if (_room.UnderRenovation) return false;

            bool free = true;
            foreach (Appointment a in _room.Appointments)
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

        public bool IsUnderRenovation(DateTime startDate, DateTime endDate)
        {
            foreach (Renovation r in _room.Renovations)
            {
                if ((r.StartDate < endDate && r.StartDate > startDate) || (r.EndDate < endDate && r.EndDate > startDate))
                {
                    return true;
                }
            }
            return false;
        }

        public void Change(string newName, int newNumber, RoomType newType)
        {
            if (newName is null || newName.Equals("")) throw new EmptyNameException("Room name cannot be empty");
            else if (newNumber == 0) throw new ZeroRoomNumberException("Room number cannot be 0");
            else if (!Institution.Instance().RoomRepository.CheckNumber(newNumber, new List<int> { _room.Number })) throw new RoomNumberAlreadyTakenException("Room number already taken");
            else if (newType != _room.Type && !IsChangeable()) throw new RoomCannotBeChangedException("Room cannot be changed, because it has scheduled appointments");
            _room.Name = newName;
            _room.Number = newNumber;
            _room.Type = newType;
        }
    }
}