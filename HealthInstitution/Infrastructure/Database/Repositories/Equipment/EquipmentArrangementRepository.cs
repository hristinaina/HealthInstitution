using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;


namespace HealthInstitution.Infrastructure.Database.Repositories
{
    public class EquipmentArrangementRepository : BaseRepository, IEquipmentArrangementRepository
    {
        private List<EquipmentArrangement> _validArrangement;

        public List<EquipmentArrangement> ValidArrangement { get => _validArrangement; }

        public List<EquipmentArrangement> CurrentArrangement
        {
            get
            {
                List<EquipmentArrangement> currentArrangement = new List<EquipmentArrangement>();
                foreach (EquipmentArrangement a in _validArrangement)
                {
                    if (a.StartDate <= DateTime.Today && a.EndDate > DateTime.Today) currentArrangement.Add(a);
                }
                return currentArrangement;
            }
        }

        public EquipmentArrangementRepository(string fileName)
        {
            _fileName = fileName;
            _validArrangement = new List<EquipmentArrangement>();
        }

        public override void LoadFromFile()
        {
            List<EquipmentArrangement> allArragments = FileService.Deserialize<EquipmentArrangement>(_fileName);
            if (_validArrangement is null) _validArrangement = new List<EquipmentArrangement>();

            foreach (EquipmentArrangement a in allArragments)
            {
                if (a.IsValid()) _validArrangement.Add(a);
            }
        }

        public override void SaveToFile()
        {
            FileService.Serialize<EquipmentArrangement>(_fileName, _validArrangement);
        }

        public EquipmentArrangement FindCurrentArrangement(Room r, Equipment e)
        {
            foreach (EquipmentArrangement a in CurrentArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID) return a;
            }
            return null;
        }

        public EquipmentArrangement FindFirstBefore(Room r, Equipment e, DateTime date)
        {
            EquipmentArrangement arrangement = null;
            foreach (EquipmentArrangement a in ValidArrangement)
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
            foreach (EquipmentArrangement a in ValidArrangement)
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
            foreach (EquipmentArrangement a in ValidArrangement)
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
            foreach (EquipmentArrangement a in ValidArrangement)
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
            foreach (EquipmentArrangement arrangement in CurrentArrangement)
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
