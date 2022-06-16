using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
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
