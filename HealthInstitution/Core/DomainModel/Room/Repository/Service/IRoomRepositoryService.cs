using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IRoomRepositoryService
    {
        public Room FindById(int id);

        public bool CheckNumber(int number, List<int> ignore);

        public int GetNewID();

        public Room AddRoom(Room room, bool future = false, List<int> ignoredNumbers = null);

        public void DeleteRoom(Room r);

        public List<Room> GetCurrentRooms();
        public List<Room> GetDeletedRooms();
        public List<Room> GetFutureRooms();

        public void Change(string newName, int newNumber, RoomType newType, Room room);

        public bool IsUnderRenovation(DateTime startDate, DateTime endDate, Room room);

        public bool isAvailable(DateTime appointmentTime, Appointment appointment, Room room);

        public void ReturnEquipmentToWarehouse(DateTime date, Room room);

        public void AddEquipment(Equipment e, int quantity, Room room);

        public bool IsChangeable(Room room);
    }
}