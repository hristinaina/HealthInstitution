using System;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories.Room;

namespace HealthInstitution.MVVM.Models.Services.Equipments
{
    public class EquipmentArrangementService
    {
        private EquipmentArrangementRepository _arrangements;

        public EquipmentArrangementService()
        {
            _arrangements = Institution.Instance().EquipmentArragmentRepository;
        }

        public EquipmentArrangement FindCurrentArrangement(Entities.Room r, Equipment e)
        {
            foreach (EquipmentArrangement a in _arrangements.CurrentArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID) return a;
            }
            return null;
        }

        public EquipmentArrangement FindFirstBefore(Entities.Room r, Equipment e, DateTime date)
        {
            EquipmentArrangement arrangement = null;
            foreach (EquipmentArrangement a in _arrangements.ValidArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate < date)
                {
                    if (arrangement is null || a.StartDate > arrangement.StartDate) arrangement = a;
                }
            }
            return arrangement;
        }

        public List<EquipmentArrangement> FindAllBefore(Entities.Room r, Equipment e, DateTime date)
        {
            List<EquipmentArrangement> arrangements = new List<EquipmentArrangement>();
            foreach (EquipmentArrangement a in _arrangements.ValidArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate < date)
                {
                    arrangements.Add(a);
                }
            }
            return arrangements;
        }

        public EquipmentArrangement FindFirstAfter(Entities.Room r, Equipment e, DateTime date)
        {
            EquipmentArrangement arrangement = null;
            foreach (EquipmentArrangement a in _arrangements.ValidArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate > date)
                {
                    if (arrangement is null || a.StartDate < arrangement.StartDate) arrangement = a;
                }
            }
            return arrangement;
        }

        public List<EquipmentArrangement> FindAllAfter(Entities.Room r, Equipment e, DateTime date)
        {
            List<EquipmentArrangement> arrangements = new List<EquipmentArrangement>();
            foreach (EquipmentArrangement a in _arrangements.ValidArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID && a.StartDate > date)
                {
                    arrangements.Add(a);
                }
            }
            return arrangements;
        }

        public bool UpdateEquipmentQuantityInRoom(Entities.Room room, Equipment equipment)
        {
            foreach (EquipmentArrangement arrangement in _arrangements.CurrentArrangement)
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