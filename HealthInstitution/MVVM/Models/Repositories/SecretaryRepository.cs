using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class SecretaryRepository
    {
        private string _secretaryFileName;
        private List<SecretaryController> _secretaries;

        public SecretaryRepository(string secretaryFileName)
        {
            this._secretaryFileName = secretaryFileName;
            this._secretaries = new List<SecretaryController>();
        }

        public List<SecretaryController> GetSecretaries()
        {
            return this._secretaries;
        }

        public bool LoadFromFile()
        {
            // TODO: implementirati funkciju za ucitavanje podataka iz fajla
            return false;
        }

        public bool SaveToFile()
        {
            // TODO: implementirati funkciju za cuvanje podataka u fajl
            return false;
        }
    }
}
