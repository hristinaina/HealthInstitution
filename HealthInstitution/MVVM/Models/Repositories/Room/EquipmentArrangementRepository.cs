using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    if (a.StartDate < DateTime.Today && a.EndDate > DateTime.Today) currentArrangement.Add(a);
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
                if (a.EndDate > DateTime.Today) _validArrangement.Add(a);
            }
        }

        public void SaveToFile()
        {
            FileService.Serialize<EquipmentArrangement>(_fileName, _validArrangement);
        }

        public EquipmentArrangement FindByRoomAndEquipment(Entities.Room r, Equipment e)
        {
            foreach (EquipmentArrangement a in _validArrangement)
            {
                if (a.RoomId == r.ID && a.EquipmentId == e.ID) return a;
            }
            return null;
        }

    }
}
