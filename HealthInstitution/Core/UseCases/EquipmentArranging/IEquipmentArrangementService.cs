using System;
using HealthInstitution.Core;

namespace HealthInstitution.Desktop.MVVM.Models.Services.Equipments
{
    public interface IEquipmentArrangementService
    {
        public void ArrangeInRoom(Room r, int quantity, Equipment equipment);

        public bool UpdateEquipmentQuantityInRoom(Room room, Equipment equipment);

        public void ReturnEquipmentToWarehouse(DateTime date, Room room, Equipment equipment);
    }
}