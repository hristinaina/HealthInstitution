using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Enumerations;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Perscription
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


        public Perscription(Medicine medicine, int longitudeInDays, int dailyFrequency,
                            TherapyMealDependency mealDependency)
        {
            _medicine = medicine;
            _longitudeInDays = longitudeInDays;
            _dailyFrequency = dailyFrequency;
            _therapyMealDependency = mealDependency;
        }

        public int GetID() => _id;
        public void SetID(int id) => _id = id;
        public Medicine GetMedicine() => _medicine;
        public void SetMedicine(Medicine medicine) => _medicine = medicine;
        public int GetLongitudeInDays() => _longitudeInDays;
        public void SetLongitudeInDays(int numberOfDays) => _longitudeInDays = numberOfDays;
        public int GetDailyFrequency() => _dailyFrequency;
        public void SetDailyFrequency(int dailyFrequency) => _dailyFrequency = dailyFrequency;
        public TherapyMealDependency GetTherapyMealDependency() => _therapyMealDependency;
        public void SetTherapyMealDependency(TherapyMealDependency mealDependency) 
                                             => _therapyMealDependency = mealDependency;

    }
}
