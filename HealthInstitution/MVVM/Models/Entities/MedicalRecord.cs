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

        public double Height { get => this._height; set { this._height = value; } }
        public double Weight { get => this._weight; set { this._weight = value; } }
        // history of sickness !?
        [JsonIgnore]
        public List<Allergen> Allergens{
            get
            {
                if (this._allergens == null) this._allergens = new List<Allergen>();
                return this._allergens;
            }
            set
            {
                this._allergens = value;
            }
        }
        [JsonIgnore]
        public List<Appointment> Appointments{
            get
            {
                if (this._appointments == null) this._appointments = new List<Appointment>();
                return this._appointments;
            }
            set
            {
                this._appointments = value;
            }
        }

        public MedicalRecord()
        {
        }

        public MedicalRecord(double height, double weight)
        {
            this._height = height;
            this._weight = weight;
        }

        public MedicalRecord(double height, double weight, List<Allergen> allergens) : this(height, weight)
        {
            this._allergens = allergens;
        }
    }
}
