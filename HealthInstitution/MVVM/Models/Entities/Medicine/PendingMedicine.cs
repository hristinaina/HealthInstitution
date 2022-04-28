using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Enumerations;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class PendingMedicine
    {
        private int _id;
        private string _name;
        private string _description;
        private State _state;

        public int Id { get => _id; set { _id = value; } }
        public string Name { get => _name; set { _name = value; } }
        public string Description { get => _description; set { _description = value; } }
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
