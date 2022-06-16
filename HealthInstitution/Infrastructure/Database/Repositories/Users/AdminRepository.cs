using System.Collections.Generic;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class AdminRepository : BaseRepository
    {
        private List<Admin> _administrators;

        public List<Admin> Administrators { get => _administrators; }
        public AdminRepository(string fileName)
        {
            _fileName = fileName;
            _administrators = new List<Admin>();
        }

        public override void LoadFromFile()
        {
            _administrators = FileService.Deserialize<Admin>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<Admin>(_fileName, _administrators);
        }
    }
}
