using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Infrastructure.Database.Repositories;

namespace HealthInstitution.Core.Services.Equipments
{
    public class EquipmentService
    {
        private IEquipmentArrangementRepositoryService _arrangements;
        private IRoomRepositoryService _rooms;

        public EquipmentService()
        {
            _arrangements = new EquipmentArrangementRepositoryService();
            _rooms = new RoomRepositoryService();
        }

        public void ArrangeInRoom(Room r, int quantity, Equipment equipment)
        {
            if (!equipment.ArrangmentByRooms.ContainsKey(r))
            {
                equipment.ArrangmentByRooms[r] = 0;
            }
            equipment.ArrangmentByRooms[r] += quantity;
        }

        public int GetQuantityInRoom(Room r, Equipment equipment)
        {
            return equipment.ArrangmentByRooms[r];
        }

        public void ReturnToWarehouse(DateTime date, Room room, Equipment equipment)
        {
            Room warehouse = _rooms.FindById(0);

            IEquipmentArrangementRepositoryService service = new EquipmentArrangementRepositoryService();
            EquipmentArrangement destinationRoomArrangement = service.FindFirstBefore(room, equipment, date);
            List<EquipmentArrangement> futureArrangements = service.FindAllAfter(room, equipment, date);

            destinationRoomArrangement.EndDate = date;
            foreach (EquipmentArrangement a in futureArrangements)
            {
                _arrangements.GetArrangements().Remove(a);
            }

            EquipmentArrangement warehouseArrangement = service.FindFirstBefore(warehouse, equipment, date);
            futureArrangements = service.FindAllAfter(warehouse, equipment, date);
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

        public void Rearrange(Room destinationRoom, Room targetRoom, DateTime newArrangementStartDate, int newArrangementQuantity, Equipment equipment)
        {
            User user = Institution.Instance().CurrentUser;
            if (targetRoom is null) throw new RearrangeTargetRoomNullException("Target room must be selected");
            else if (newArrangementQuantity == 0) throw new ZeroQuantityException("Quantity cannot be zero");
            else if (newArrangementStartDate <= DateTime.Today && user is Admin) throw new DateException("Arrangement date must be in future");
            else if (newArrangementQuantity > equipment.ArrangmentByRooms[destinationRoom] && user is Admin) throw new NotEnoughEquipmentException("Not enough equipment in selected room");
            if (user is Admin)
            {
                MoveFromRoom(destinationRoom, newArrangementStartDate, newArrangementQuantity, equipment);
                MoveToNewRoom(targetRoom, newArrangementStartDate, newArrangementQuantity, equipment);
            }
            else
            {
                MoveFromRoom(targetRoom, newArrangementStartDate, newArrangementQuantity, equipment);
                MoveToNewRoom(destinationRoom, newArrangementStartDate, newArrangementQuantity, equipment);
            }
        }

        private bool MoveFromWarehouse(Room room, int quantity, Equipment equipment)
        {
            if (Institution.Instance().CurrentUser is not Secretary) return false;
            if (room.ID != 0) return false;
            if (quantity > equipment.Quantity)
                throw new NotEnoughEquipmentException("Not enough equipment in warehouse");
            equipment.Quantity -= quantity;
            return true;
        }

        private bool UpdateEquipmentInRoom(EquipmentArrangement pastArrangement, Room room, int quantity, Equipment equipment)
        {
            if (Institution.Instance().CurrentUser is Secretary)
            {
                pastArrangement.Quantity = quantity;
                equipment.ArrangmentByRooms[room] = quantity;
                pastArrangement.EndDate = DateTime.MaxValue;
                return true;
            };

            return false;
        }

        private void MoveFromRoom(Room room, DateTime newArrangementStartDate, int quantity, Equipment equipment)
        {
            if (MoveFromWarehouse(room, quantity, equipment)) return;
            if (Institution.Instance().CurrentUser is Secretary && quantity > equipment.ArrangmentByRooms[room]) throw new NotEnoughEquipmentException("Not enough equipment in selected room");

            IEquipmentArrangementRepositoryService service = new EquipmentArrangementRepositoryService();
            EquipmentArrangement pastArrangement = service.FindFirstBefore(room, equipment, newArrangementStartDate);
            List<EquipmentArrangement> futureArrangements = service.FindAllAfter(room, equipment, newArrangementStartDate);

            DateTime newArrangementEndDate = pastArrangement.EndDate;
            pastArrangement.EndDate = newArrangementStartDate;
            foreach (EquipmentArrangement a in futureArrangements)
            {
                a.Quantity -= quantity;
            }

            int newDestinationRoomQuantity = pastArrangement.Quantity - quantity;
            if (UpdateEquipmentInRoom(pastArrangement, room, newDestinationRoomQuantity, equipment)) return;
            _arrangements.GetArrangements().Add(new EquipmentArrangement(equipment, room, newDestinationRoomQuantity, newArrangementStartDate, newArrangementEndDate));
        }

        private void MoveToNewRoom(Room room, DateTime newArrangementStartDate, int quantity, Equipment equipment)
        {
            IEquipmentArrangementRepositoryService service = new EquipmentArrangementRepositoryService();
            EquipmentArrangement pastArrangement = service.FindFirstBefore(room, equipment, newArrangementStartDate);
            List<EquipmentArrangement> futureArrangements = service.FindAllAfter(room, equipment, newArrangementStartDate);
            DateTime newArrangementTargetEndDate = DateTime.MaxValue;

            if (pastArrangement is not null)
            {
                newArrangementTargetEndDate = pastArrangement.EndDate;
                pastArrangement.EndDate = newArrangementStartDate;
            }

            foreach (EquipmentArrangement a in futureArrangements)
            {
                a.Quantity += quantity;
            }

            int newTargetRoomQuantity = 0;
            if (pastArrangement is not null)
            {
                newTargetRoomQuantity = pastArrangement.Quantity;
            }
            newTargetRoomQuantity += quantity;
            if (UpdateEquipmentInRoom(pastArrangement, room, newTargetRoomQuantity, equipment)) return;
            _arrangements.GetArrangements().Add(new EquipmentArrangement(equipment, room, newTargetRoomQuantity, newArrangementStartDate, newArrangementTargetEndDate));
        }
    }
}