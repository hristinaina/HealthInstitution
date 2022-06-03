using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;


namespace HealthInstitution.MVVM.Models.Repositories.Room
{
    public class EquipmentArrangementRepository
    {
        private readonly string _fileName;
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

        public void LoadFromFile()
        {
            List<EquipmentArrangement> allArragments = FileService.Deserialize<EquipmentArrangement>(_fileName);
            if (_validArrangement is null) _validArrangement = new List<EquipmentArrangement>();

            foreach (EquipmentArrangement a in allArragments)
            {
                if (a.IsValid()) _validArrangement.Add(a);
            }
        }

        public void SaveToFile()
        {
            FileService.Serialize<EquipmentArrangement>(_fileName, _validArrangement);
        }

        public EquipmentArrangement FindCurrentArrangement(Entities.Room r, Equipment e)
        {
            foreach (EquipmentArrangement a in CurrentArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID) return a;
            }
            return null;
        }

        public EquipmentArrangement FindFirstBefore(Entities.Room r, Equipment e, DateTime date)
        {
            EquipmentArrangement arrangement = null;
            foreach (EquipmentArrangement a in _validArrangement)
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
            foreach (EquipmentArrangement a in _validArrangement)
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
            foreach (EquipmentArrangement a in _validArrangement)
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
            foreach (EquipmentArrangement a in _validArrangement)
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
