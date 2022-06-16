using System;
using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Infrastructure.Database.Repositories;

namespace HealthInstitution.Core.Services.Equipments
{
    public class EquipmentArrangementService
    {
        private IEquipmentArrangementRepositoryService _arrangements;

        public EquipmentArrangementService()
        {
            _arrangements = new EquipmentArrangementRepositoryService();
        }

        public EquipmentArrangement FindCurrentArrangement(Room r, Equipment e)
        {
            foreach (EquipmentArrangement a in _arrangements.GetCurrentArrangements())
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID) return a;
            }
            return null;
        }

        public EquipmentArrangement FindFirstBefore(Room r, Equipment e, DateTime date)
        {
            EquipmentArrangement arrangement = null;
            foreach (EquipmentArrangement a in _arrangements.GetArrangements())
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate < date)
                {
                    if (arrangement is null || a.StartDate > arrangement.StartDate) arrangement = a;
                }
            }
            return arrangement;
        }

        public List<EquipmentArrangement> FindAllBefore(Room r, Equipment e, DateTime date)
        {
            List<EquipmentArrangement> arrangements = new List<EquipmentArrangement>();
            foreach (EquipmentArrangement a in _arrangements.GetArrangements())
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate < date)
                {
                    arrangements.Add(a);
                }
            }
            return arrangements;
        }

        public EquipmentArrangement FindFirstAfter(Room r, Equipment e, DateTime date)
        {
            EquipmentArrangement arrangement = null;
            foreach (EquipmentArrangement a in _arrangements.GetArrangements())
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate > date)
                {
                    if (arrangement is null || a.StartDate < arrangement.StartDate) arrangement = a;
                }
            }
            return arrangement;
        }

        public List<EquipmentArrangement> FindAllAfter(Room r, Equipment e, DateTime date)
        {
            List<EquipmentArrangement> arrangements = new List<EquipmentArrangement>();
            foreach (EquipmentArrangement a in _arrangements.GetArrangements())
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate > date)
                {
                    arrangements.Add(a);
                }
            }
            return arrangements;
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
    }
}