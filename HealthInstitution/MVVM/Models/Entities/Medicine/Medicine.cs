using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Medicine
    {
        private int _id { set; get; }
        private string _name { set; get; }
        private List<Allergen> _allergens { set; get; }

        [JsonProperty("ID")]
        public int ID { get => this._id; set { this._id = value; } }
        [JsonProperty("Name")]

        public string Name { get => this._name; set { this._name = value; } }
        
        [JsonIgnore]
        public List<Allergen> Allergens { get => this._allergens; set { this._allergens = value; } }

        public Medicine(int id, string name)
        {
            _id = id;
            _name = name;
            _allergens = new List<Allergen>();
        }

        public int GetId() => _id;
        public void SetId(int id) => _id = id;
        public string GetName() => _name;
        public void SetName(string name) => _name = name;
        public List<Allergen> GetAllergens() => _allergens;
        public void SetAllegens(List<Allergen> allergens) => _allergens = allergens;
    }

}
