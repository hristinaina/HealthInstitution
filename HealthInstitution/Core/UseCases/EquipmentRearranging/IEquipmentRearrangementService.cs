using System;
using HealthInstitution.Core;

namespace HealthInstitution.Desktop.MVVM.Models.Services.Equipments
{
    public interface IEquipmentRearrangementService
    {
        public void Rearrange(Room destinationRoom, Room targetRoom, DateTime newArrangementStartDate,
            int newArrangementQuantity, Equipment equipment);
    }
}