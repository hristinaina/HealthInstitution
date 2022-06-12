using HealthInstitution.Core.Exceptions;
using HealthInstitution.Core;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using System.Collections.Generic;

namespace HealthInstitution.Core
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
    }
}