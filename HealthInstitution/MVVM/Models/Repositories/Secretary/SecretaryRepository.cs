using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class SecretaryRepository
    {
        private string _secretaryFileName;
        private List<Secretary> _secretaries;

        public SecretaryRepository(string secretaryFileName)
        {
            this._secretaryFileName = secretaryFileName;
            this._secretaries = new List<Secretary
                >();
        }

        public List<Secretary> GetSecretaries()
        {
            return this._secretaries;
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
