using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Enumerations;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Prescription
    {
        private int _id;
        private Medicine _medicine;
        private int _longitudeInDays;     // how long to take the medicine
        private int _dailyFrequency;      // how many times a day to take a medicine
        private TherapyMealDependency _therapyMealDependency;

        [JsonProperty("ID")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("LongitudeInDays")]
        public int LongitudeInDays { get => _longitudeInDays; set { _longitudeInDays = value; } }
        [JsonProperty("TimesADay")]
        public int TimesADat { get => _dailyFrequency; set { _dailyFrequency = value; } }
        [JsonProperty("TherapyMealDependency")]
        public TherapyMealDependency TherapyMealDependency { get => _therapyMealDependency;
                                                             set { _therapyMealDependency = value; } }
        [JsonIgnore]
        public Medicine Medicine { get => _medicine; set { _medicine = value; } }
   
       
        public Prescription()
        {

        }

        public Prescription(int id, int longitudeInDays, int dailyFrequency,
                            TherapyMealDependency mealDependency, Medicine medicines=null)
        {
            _id = id;
            _longitudeInDays = longitudeInDays;
            _dailyFrequency = dailyFrequency;
            _therapyMealDependency = mealDependency;
            _medicine = medicines;
            if (_medicine == null) _medicine = new Medicine(-1, " ");
        }

        public Prescription(int id)
        {
            ID = id;
        }
    }
}
