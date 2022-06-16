using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using HealthInstitution.Core;


namespace HealthInstitution.Infrastructure.Database.Repositories
{
    public class EquipmentArrangementRepository : BaseRepository
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

    }
}
