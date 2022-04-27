using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class PerscriptionRepository
    {
        private string _fileName;
        private List<Perscription> _perscriptions;

        public PerscriptionRepository(string filePath)
        {
            _fileName = filePath;
            _perscriptions = new List<Perscription>();
        }

        public List<Perscription> GetPerscriptions()
        {
            return _perscriptions;
        }

        public void LoadFromFile()
        {
            _perscriptions = FileService.Deserialize<Perscription>(_fileName);

        }

        public void SaveToFile()
        {
            FileService.Serialize<Perscription>(_fileName, _perscriptions);
        }
        public Perscription FindByID(int id)
        {
            foreach (Perscription perscription in _perscriptions)
            {
                if (perscription.ID == id) return perscription;
            }
            return null;
        }
    }
}
