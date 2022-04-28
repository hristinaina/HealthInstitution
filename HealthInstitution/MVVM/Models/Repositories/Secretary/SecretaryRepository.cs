using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class SecretaryRepository
    {
        private readonly string _secretaryFileName;
        private List<Secretary> _secretaries;

        public List<Secretary> Secretaries { get => _secretaries; }
        public SecretaryRepository(string secretaryFileName)
        {
            _secretaryFileName = secretaryFileName;
            _secretaries = new List<Secretary
                >();
        }

        public void LoadFromFile()
        {
            _secretaries = FileService.Deserialize<Secretary>(_secretaryFileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Secretary>(_secretaryFileName, _secretaries);
        }
    }
}
