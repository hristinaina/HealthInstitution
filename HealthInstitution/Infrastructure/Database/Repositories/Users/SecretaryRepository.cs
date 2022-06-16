using System.Collections.Generic;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class SecretaryRepository : BaseRepository
    {
        private List<Secretary> _secretaries;

        public List<Secretary> Secretaries { get => _secretaries; }
        public SecretaryRepository(string fileName)
        {
            _fileName = fileName;
            _secretaries = new List<Secretary
                >();
        }

        public override void LoadFromFile()
        {
            _secretaries = FileService.Deserialize<Secretary>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<Secretary>(_fileName, _secretaries);
        }
    }
}
