using System.Collections.Generic;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class AdminRepository
    {
        private readonly string _adminFileName;
        private List<Admin> _administrators;

        public List<Admin> Administrators { get => _administrators; }
        public AdminRepository(string adminFileName)
        {
            _adminFileName = adminFileName;
            _administrators = new List<Admin>();
        }

        public void LoadFromFile()
        {
            _administrators = FileService.Deserialize<Admin>(_adminFileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Admin>(_adminFileName, _administrators);
        }
    }
}
