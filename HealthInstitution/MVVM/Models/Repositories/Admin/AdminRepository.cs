using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class AdminRepository
    {
        private string _adminFileName;
        private List<Admin> _administrators;

        public AdminRepository(string adminFileName)
        {
            this._adminFileName = adminFileName;
            this._administrators = new List<Admin>();
        }

        public List<Admin> GetAdministrators()
        {
            return this._administrators;
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
