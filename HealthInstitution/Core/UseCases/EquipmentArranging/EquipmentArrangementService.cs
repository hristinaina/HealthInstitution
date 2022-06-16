using System;
using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Desktop.MVVM.Models.Services.Equipments;
using HealthInstitution.Infrastructure.Database.Repositories;

namespace HealthInstitution.Core.Services.Equipments
{
    public class EquipmentArrangementService : IEquipmentArrangementService
    {
        private IEquipmentArrangementRepositoryService _arrangements;
        private IRoomRepositoryService _rooms;

        public EquipmentArrangementService()
        {
            _arrangements = new EquipmentArrangementRepositoryService();
            _rooms = new RoomRepositoryService();
        }

        public bool UpdateEquipmentQuantityInRoom(Room room, Equipment equipment)
        {
            foreach (EquipmentArrangement arrangement in _arrangements.GetCurrentArrangements())
            {
                if (arrangement.RoomId == room.ID && arrangement.EquipmentId == equipment.ID)
                {
                    arrangement.Quantity = equipment.ArrangmentByRooms[room];
                    return true;
                }
            }
            return false;
        }

        public void ReturnEquipmentToWarehouse(DateTime date, Room room, Equipment equipment)
        {
            Room warehouse = _rooms.FindById(0);

            EquipmentArrangement destinationRoomArrangement = _arrangements.FindFirstBefore(room, equipment, date);
            List<EquipmentArrangement> futureArrangements = _arrangements.FindAllAfter(room, equipment, date);

            destinationRoomArrangement.EndDate = date;
            foreach (EquipmentArrangement a in futureArrangements)
            {
                _arrangements.GetArrangements().Remove(a);
            }

            EquipmentArrangement warehouseArrangement = _arrangements.FindFirstBefore(warehouse, equipment, date);
            futureArrangements = _arrangements.FindAllAfter(warehouse, equipment, date);
            DateTime newArrangementTargetEndDate = DateTime.MaxValue;


            if (warehouseArrangement is not null)
            {
                newArrangementTargetEndDate = warehouseArrangement.EndDate;
                warehouseArrangement.EndDate = date;
            }

            foreach (EquipmentArrangement a in futureArrangements)
            {
                a.Quantity += destinationRoomArrangement.Quantity;
            }

            int newWarehouseQuantity = 0;
            if (warehouseArrangement is not null)
            {
                newWarehouseQuantity = warehouseArrangement.Quantity;
            }
            newWarehouseQuantity += destinationRoomArrangement.Quantity;

            _arrangements.GetArrangements().Add(new EquipmentArrangement(equipment, warehouse, newWarehouseQuantity, date, newArrangementTargetEndDate));

        }

        public void ArrangeInRoom(Room r, int quantity, Equipment equipment)
        {
            if (!equipment.ArrangmentByRooms.ContainsKey(r))
            {
                equipment.ArrangmentByRooms[r] = 0;
            }
            equipment.ArrangmentByRooms[r] += quantity;
        }
    }
}