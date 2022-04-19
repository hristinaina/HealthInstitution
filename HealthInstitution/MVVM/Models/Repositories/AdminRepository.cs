using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities.Admin;

namespace HealthInstitution.MVVM.Models.Repositories
{
    class AdminRepository
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
