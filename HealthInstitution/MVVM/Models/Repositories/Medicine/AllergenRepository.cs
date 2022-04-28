using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    class AllergenRepository
    {
        private string _fileName;
        private List<Allergen> _allergen;
        public List<Allergen> Allergen { get => this._allergen; }

        public AllergenRepository(string fileName)
        {
            this._fileName = fileName;
            this._allergen = new List<Allergen>();
        }

        public void LoadFromFile()
        {
            _allergen = FileService.Deserialize<Allergen>(this._fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Allergen>(this._fileName, this._allergen);
        }
        public Allergen FindByID(int id)
        {
            foreach (Allergen allergen in _allergen)
            {
                if (allergen.Id == id) return allergen;
            }
            return null;
        }
    }
}
