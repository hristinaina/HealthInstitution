using System;
using System.Collections.Generic;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Infrastructure.Database.Repositories;

namespace HealthInstitution.Core.Services.Equipments
{
    public class EquipmentService
    {
        private Equipment _equipment;
        private EquipmentArrangementRepository _arrangements;

        public EquipmentService(Equipment e)
        {
            _equipment = e;
            _arrangements = Institution.Instance().EquipmentArragmentRepository;
        }

        public void ArrangeInRoom(Room r, int quantity)
        {
            if (!_equipment.ArrangmentByRooms.ContainsKey(r))
            {
                _equipment.ArrangmentByRooms[r] = 0;
            }
            _equipment.ArrangmentByRooms[r] += quantity;
        }

        public int GetQuantityInRoom(Room r)
        {
            return _equipment.ArrangmentByRooms[r];
        }

        public void ReturnToWarehouse(DateTime date, Room room)
        {
            Room warehouse = Institution.Instance().RoomRepository.FindById(0);

            EquipmentArrangementService service = new EquipmentArrangementService();
            EquipmentArrangement destinationRoomArrangement = service.FindFirstBefore(room, _equipment, date);
            List<EquipmentArrangement> futureArrangements = service.FindAllAfter(room, _equipment, date);

            destinationRoomArrangement.EndDate = date;
            foreach (EquipmentArrangement a in futureArrangements)
            {
                Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Remove(a);
            }

            EquipmentArrangement warehouseArrangement = service.FindFirstBefore(warehouse, _equipment, date);
            futureArrangements = service.FindAllAfter(warehouse, _equipment, date);
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

            _arrangements.ValidArrangement.Add(new EquipmentArrangement(_equipment, warehouse, newWarehouseQuantity, date, newArrangementTargetEndDate));

        }

        public void Rearrange(Room destinationRoom, Room targetRoom, DateTime newArrangementStartDate, int newArrangementQuantity)
        {
            User user = Institution.Instance().CurrentUser;
            if (targetRoom is null) throw new RearrangeTargetRoomNullException("Target room must be selected");
            else if (newArrangementQuantity == 0) throw new ZeroQuantityException("Quantity cannot be zero");
            else if (newArrangementStartDate <= DateTime.Today && user is Admin) throw new DateException("Arrangement date must be in future");
            else if (newArrangementQuantity > _equipment.ArrangmentByRooms[destinationRoom] && user is Admin) throw new NotEnoughEquipmentException("Not enough equipment in selected room");
            if (user is Admin)
            {
                MoveFromRoom(destinationRoom, newArrangementStartDate, newArrangementQuantity);
                MoveToNewRoom(targetRoom, newArrangementStartDate, newArrangementQuantity);
            }
            else
            {
                MoveFromRoom(targetRoom, newArrangementStartDate, newArrangementQuantity);
                MoveToNewRoom(destinationRoom, newArrangementStartDate, newArrangementQuantity);
            }
        }

        private bool MoveFromWarehouse(Room room, int quantity)
        {
            if (Institution.Instance().CurrentUser is not Secretary) return false;
            if (room.ID != 0) return false;
            if (quantity > _equipment.Quantity)
                throw new NotEnoughEquipmentException("Not enough equipment in warehouse");
            _equipment.Quantity -= quantity;
            return true;
        }

        private bool UpdateEquipmentInRoom(EquipmentArrangement pastArrangement, Room room, int quantity)
        {
            if (Institution.Instance().CurrentUser is Secretary)
            {
                pastArrangement.Quantity = quantity;
                _equipment.ArrangmentByRooms[room] = quantity;
                pastArrangement.EndDate = DateTime.MaxValue;
                return true;
            };

            return false;
        }

        private void MoveFromRoom(Room room, DateTime newArrangementStartDate, int quantity)
        {
            if (MoveFromWarehouse(room, quantity)) return;
            if (Institution.Instance().CurrentUser is Secretary && quantity > _equipment.ArrangmentByRooms[room]) throw new NotEnoughEquipmentException("Not enough equipment in selected room");

            EquipmentArrangementService service = new EquipmentArrangementService();
            EquipmentArrangement pastArrangement = service.FindFirstBefore(room, _equipment, newArrangementStartDate);
            List<EquipmentArrangement> futureArrangements = service.FindAllAfter(room, _equipment, newArrangementStartDate);

            DateTime newArrangementEndDate = pastArrangement.EndDate;
            pastArrangement.EndDate = newArrangementStartDate;
            foreach (EquipmentArrangement a in futureArrangements)
            {
                a.Quantity -= quantity;
            }

            int newDestinationRoomQuantity = pastArrangement.Quantity - quantity;
            if (UpdateEquipmentInRoom(pastArrangement, room, newDestinationRoomQuantity)) return;
            _arrangements.ValidArrangement.Add(new EquipmentArrangement(_equipment, room, newDestinationRoomQuantity, newArrangementStartDate, newArrangementEndDate));
        }

        private void MoveToNewRoom(Room room, DateTime newArrangementStartDate, int quantity)
        {
            EquipmentArrangementService service = new EquipmentArrangementService();
            EquipmentArrangement pastArrangement = service.FindFirstBefore(room, _equipment, newArrangementStartDate);
            List<EquipmentArrangement> futureArrangements = service.FindAllAfter(room, _equipment, newArrangementStartDate);
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
            if (UpdateEquipmentInRoom(pastArrangement, room, newTargetRoomQuantity)) return;
            _arrangements.ValidArrangement.Add(new EquipmentArrangement(_equipment, room, newTargetRoomQuantity, newArrangementStartDate, newArrangementTargetEndDate));
        }
    }
}