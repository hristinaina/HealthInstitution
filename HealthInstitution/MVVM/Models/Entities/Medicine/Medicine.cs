using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Enumerations;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Medicine
    {
        private int _id;
        private string _name;
        private List<Allergen> _ingredients;
        private State _state;

        [JsonProperty("ID")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("Name")]

        public string Name { get => _name; set { _name = value; } }
        
        [JsonIgnore]
        public List<Allergen> Ingredients { get => _ingredients; set { _ingredients = value; } }

        public State State
        {
            get => _state;
            set => _state = value;
        }

        public Medicine(int id, string name)
        {
            _id = id;
            _name = name;
            _ingredients = new List<Allergen>();
        }

        public override string ToString()
        {
            return _name;
        }
    }

}
