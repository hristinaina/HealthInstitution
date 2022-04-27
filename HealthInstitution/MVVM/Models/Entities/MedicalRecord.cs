using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class MedicalRecord
    {
        private double _height;
        private double _weight;
        private List<Allergen> _allergens;
        private List<Appointment> _appointments;
        // TODO: add refferals

        public double Height { get => _height; set { _height = value; } }
        public double Weight { get => _weight; set { _weight = value; } }
        // history of sickness !?
        [JsonIgnore]
        public List<Allergen> Allergens{
            get
            {
                if (_allergens == null) _allergens = new List<Allergen>();
                return _allergens;
            }
            set
            {
                _allergens = value;
            }
        }
        [JsonIgnore]
        public List<Appointment> Appointments{
            get
            {
                if (_appointments == null) _appointments = new List<Appointment>();
                return _appointments;
            }
            set
            {
                _appointments = value;
            }
        }

        public MedicalRecord()
        {
        }

        public MedicalRecord(double height, double weight, List<Allergen> allergens)
        {
            _height = height;
            _weight = weight;
            _allergens = allergens;
        }
    }
}
