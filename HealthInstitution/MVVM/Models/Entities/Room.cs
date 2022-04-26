using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Room
    {
        private string _name;

        public Room(string name) {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
