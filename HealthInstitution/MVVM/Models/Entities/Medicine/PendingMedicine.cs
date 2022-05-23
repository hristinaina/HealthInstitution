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
        private string _description;
        private string _revisionDescription;
        private State _state;

        [JsonProperty("Id")]
        public int Id { get => _id; set { _id = value; } }
        [JsonProperty("Name")]
        public string Name { get => _name; set { _name = value; } }
        [JsonProperty("Description")]
        public string Description { get => _description; set { _description = value; } }
        [JsonProperty("RevisionDescription")]
        public string RevisionDescription { get => _revisionDescription; set { _revisionDescription = value; } }
        [JsonProperty("State")]
        public State State { get => _state; set { _state = value; } }


        public PendingMedicine()
        {

        }

        public PendingMedicine(int id, string name, string description, State state)
        {
            _id = id;
            _name = name;
            _description = description;
            _state = state;
        }


    }
}
