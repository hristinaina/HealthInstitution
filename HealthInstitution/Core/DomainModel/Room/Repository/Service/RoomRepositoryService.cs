using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Services.Rooms;

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
            RoomService service = new RoomService();
            if (!service.IsChangeable(r)) throw new RoomCannotBeChangedException("Room cannot be deleted, because it has scheduled appointments");
            service.ReturnEquipmentToWarehouse(DateTime.Today, r);

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
    }
}