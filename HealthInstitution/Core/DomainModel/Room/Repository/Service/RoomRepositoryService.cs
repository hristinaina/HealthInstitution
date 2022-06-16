using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services;
using HealthInstitution.Core.Services.Equipments;
using HealthInstitution.Desktop.MVVM.Models.Services.Equipments;

namespace HealthInstitution.Core.Repository
{
    public class RoomRepositoryService : IRoomRepositoryService
    {
        private IRoomRepository _repository;

        public RoomRepositoryService()
        {
            _repository = Institution.Instance().RoomRepository;
        }

        public Room FindById(int id)
        {
            return _repository.FindById(id);
        }

        public bool CheckNumber(int number, List<int> ignore)
        {
            return _repository.CheckNumber(number, ignore);
        }

        public int GetNewID()
        {
            return _repository.GetNewID();
        }

        public Room AddRoom(Room room, bool future = false, List<int> ignoredNumbers = null)
        {
            if (room.Number == 0)
            {
                throw new ZeroRoomNumberException("Room number cannot be zero");
            }
            else if (room.Name is null || room.Name.Equals(""))
            {
                throw new EmptyNameException("Room name cannot be empty");
            }
            else if (!_repository.CheckNumber(room.Number, ignoredNumbers))
            {
                throw new RoomNumberAlreadyTakenException("Room number already taken");
            }

            return _repository.AddRoom(room, future, ignoredNumbers);
        }

        public void DeleteRoom(Room r)
        {

            if (!IsChangeable(r)) throw new RoomCannotBeChangedException("Room cannot be deleted, because it has scheduled appointments");
            ReturnEquipmentToWarehouse(DateTime.Today, r);

            _repository.DeleteRoom(r);
        }

        public List<Room> GetCurrentRooms()
        {
            return _repository.GetCurrentRooms();
        }

        public List<Room> GetDeletedRooms()
        {
            return _repository.GetDeletedRooms();
        }

        public List<Room> GetFutureRooms()
        {
            return _repository.GetFutureRooms();
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
            IEquipmentArrangementService equipmentService = new EquipmentArrangementService();
            foreach (Equipment e in room.Equipment.Keys)
            {
                equipmentService.ReturnEquipmentToWarehouse(date, room, e);
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
            else if (!CheckNumber(newNumber, new List<int> { room.Number })) throw new RoomNumberAlreadyTakenException("Room number already taken");
            else if (newType != room.Type && !IsChangeable(room)) throw new RoomCannotBeChangedException("Room cannot be changed, because it has scheduled appointments");
            room.Name = newName;
            room.Number = newNumber;
            room.Type = newType;
        }
    }
}