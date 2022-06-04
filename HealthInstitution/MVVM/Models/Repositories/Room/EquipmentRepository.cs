﻿using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Services;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.Models
{
    public class EquipmentRepository
    {
        private readonly string _fileName;
        private List<Equipment> _equipment;

        public List<Equipment> Equipment { get => _equipment; }

        public EquipmentRepository(string fileName)
        {
            _fileName = fileName;
            _equipment = new List<Equipment>();
        }

        public void LoadFromFile()
        {
            _equipment = FileService.Deserialize<Equipment>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Equipment>(_fileName, _equipment);
        }

        public Equipment FindById(int id)
        {
            foreach (Equipment e in _equipment)
            {
                if (e.ID == id) return e;
            }
            return null;
        }

        public Dictionary<Equipment, List<Room>> FilterByRoomType(RoomType type)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = new Dictionary<Equipment, List<Room>>();

            foreach (Equipment e in _equipment)
            {
                foreach (Room r in e.ArrangmentByRooms.Keys)
                {
                    if (r.Type == type)
                    {
                        if (!filteredEquipment.ContainsKey(e)) filteredEquipment.Add(e, new List<Room>());
                        filteredEquipment[e].Add(r);
                    }
                }
            }
            return filteredEquipment;
        }

        public Dictionary<Equipment, List<Room>> FilterByEquipmentType(Dictionary<Equipment, List<Room>> allEquipment, EquipmentType type)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = new();

            foreach (Equipment e in allEquipment.Keys)
            {
                if (e.Type == type)
                {
                    if (!filteredEquipment.ContainsKey(e)) filteredEquipment.Add(e, new List<Room>());
                    filteredEquipment[e] = allEquipment[e]; 
                }
            }
            return filteredEquipment;
        }

        public Dictionary<Equipment, List<Room>> FilterByQuantity(Dictionary<Equipment, List<Room>> allEquipment, int minQuantity, int maxQuantity)
        {
            if (minQuantity >= maxQuantity) throw new EquipmentFilterQuantityException("Minimum quantity must be lower than maximum quantity");
            Dictionary<Equipment, List<Room>> filteredEquipment = new();
            foreach (Equipment e in allEquipment.Keys)
            {
                foreach (Room r in allEquipment[e])
                {
                    if (e.ArrangmentByRooms[r] >= minQuantity && e.ArrangmentByRooms[r] <= maxQuantity)
                    {
                        if (!filteredEquipment.ContainsKey(e)) filteredEquipment.Add(e, new List<Room>());
                        filteredEquipment[e].Add(r); 
                    }
                }
            }
            return filteredEquipment;
        }

        public Dictionary<Equipment, List<Room>> FilterEquipment(RoomType roomType, int minQuantity, int maxQuantity, EquipmentType equipmentType)
        {
            Dictionary<Equipment, List<Room>> filteredEquipment = FilterByRoomType(roomType);

            filteredEquipment = FilterByQuantity(filteredEquipment, minQuantity, maxQuantity);

            filteredEquipment = FilterByEquipmentType(filteredEquipment, equipmentType);

            return filteredEquipment;
        }
    }
}