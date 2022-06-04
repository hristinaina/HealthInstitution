using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Enumerations;
using Newtonsoft.Json;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class PendingMedicine
    {
        private int _id;
        private string _name;
        private string _revisionDescription;
        private string _description;
        private List<Allergen> _ingredients;
        private State _state;

        [JsonProperty("Id")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("Name")]
        public string Name { get => _name; set { _name = value; } }

        [JsonProperty("RevisionDescription")]
        public string RevisionDescription { get => _revisionDescription; set { _revisionDescription = value; } }

        [JsonProperty("Description")]
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        [JsonProperty("State")]
        public State State { get => _state; set { _state = value; } }
        [JsonIgnore]
        public List<Allergen> Ingredients { get => _ingredients; set { _ingredients = value; } }


        public PendingMedicine()
        {
            _ingredients = new List<Allergen>();
            _revisionDescription = "";
            _description = "";
        }

        public PendingMedicine(int id, string name, State state) : this()
        {
            _id = id;
            _name = name;
            _state = state;
        }

        public PendingMedicine(string name, List<Allergen> ingredients, State state) : this()
        {
            _name = name;
            _ingredients = ingredients;
            _state = state;
        }

    }
}
