using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories.Room
{
    public class RoomRenovationRepository
    {
        private readonly string _fileName;
        private List<RoomRenovation> _roomsUnderRenovation;

        public List<RoomRenovation> RoomsUnderRenovations { get => _roomsUnderRenovation; set => _roomsUnderRenovation = value; }

        public RoomRenovationRepository(string fileName)
        {
            _fileName = fileName;
            _roomsUnderRenovation = new List<RoomRenovation>();
        }

        public void LoadFromFile()
        {
            _roomsUnderRenovation = FileService.Deserialize<RoomRenovation>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<RoomRenovation>(_fileName, _roomsUnderRenovation);
        }

    }
}
