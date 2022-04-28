using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HealthInstitution.MVVM.Models.Entities.References
{
    public class PatientAllergen
    {
        private int _patientId;
        private int _ingredientId;

        [JsonProperty("PatientId")]
        public int PatientId { get => _patientId; set { _patientId = value; } }
        [JsonProperty("IngredientId")]
        public int IngredientId { get => _ingredientId; set { _ingredientId = value; } }

        public PatientAllergen()
        {

        }

        public PatientAllergen(int patientId, int ingredientId)
        {
            _patientId = patientId;
            _ingredientId = ingredientId;
        }
    }
}
