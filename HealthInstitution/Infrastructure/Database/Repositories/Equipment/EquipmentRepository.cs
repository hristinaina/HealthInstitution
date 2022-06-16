using HealthInstitution.Core.Services;
using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public class EquipmentRepository : BaseRepository
    {
        private List<Equipment> _equipment;

        public List<Equipment> Equipment { get => _equipment; }

        public EquipmentRepository(string fileName)
        {
            _fileName = fileName;
            _equipment = new List<Equipment>();
        }

        public override void LoadFromFile()
        {
            _equipment = FileService.Deserialize<Equipment>(_fileName);
        }

        public override void SaveToFile()
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