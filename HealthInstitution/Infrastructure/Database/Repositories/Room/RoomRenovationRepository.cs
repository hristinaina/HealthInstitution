using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using System.Collections.Generic;

namespace HealthInstitution.Infrastructure.Database.Repositories
{
    public class RoomRenovationRepository : BaseRepository
    {

        private List<RoomRenovation> _roomsUnderRenovation;

        public List<RoomRenovation> RoomsUnderRenovations { get => _roomsUnderRenovation; set => _roomsUnderRenovation = value; }

        public RoomRenovationRepository(string fileName)
        {
            _fileName = fileName;
            _roomsUnderRenovation = new List<RoomRenovation>();
        }

        public override void LoadFromFile()
        {
            _roomsUnderRenovation = FileService.Deserialize<RoomRenovation>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<RoomRenovation>(_fileName, _roomsUnderRenovation);
        }

    }
}
