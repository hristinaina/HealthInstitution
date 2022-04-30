using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HealthInstitution.MVVM.Models.Entities.References
{
    public class MedicineAllergen
    {
        private int _medicineId;
        private int _ingredientId;

        [JsonProperty("MedicineId")]
        public int MedicineId { get => _medicineId; set { _medicineId = value; } }
        [JsonProperty("IngredientId")]
        public int IngredientId { get => _ingredientId; set { _ingredientId = value; } }

        public MedicineAllergen()
        {

        }

        public MedicineAllergen(int medicineId, int ingredientId)
        {
            _medicineId = medicineId;
            _ingredientId = ingredientId;
        }
    }
}
