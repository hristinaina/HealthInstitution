using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;

namespace HealthInstitution.Core
{
    public class Medicine
    {
        private int _id;
        private string _name;
        private string _description;
        private List<Allergen> _ingredients;
        private State _state;

        [JsonProperty("ID")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("Name")]

        public string Name { get => _name; set { _name = value; } }

        [JsonProperty("Description")]
        public string Description { get => _description; set { _description = value; } }

        [JsonIgnore]
        public List<Allergen> Ingredients { get => _ingredients; set { _ingredients = value; } }

        public State State
        {
            get => _state;
            set => _state = value;
        }

        public Medicine()
        {
            _ingredients = new List<Allergen>();
        }

        public Medicine(int id, string name) : this()
        {
            _id = id;
            _name = name;
        }

        public Medicine(string name, List<Allergen> ingredients, State s) : this()
        {
            _name = name;
            _ingredients = ingredients;
            _state = s;
        }

        public Medicine(int id, string name, List<Allergen> allergens)
        {
            _id = id;
            _name = name;
            _ingredients = allergens;
        }

        public override string ToString()
        {
            return _name;
        }
    }

}
