using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;

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
