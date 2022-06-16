using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HealthInstitution.Core
{
    public class PrescriptionMedicine
    {
        private int _medicineId;
        private int _prescriptionId;

        [JsonProperty("MedicineId")]
        public int MedicineId { get => _medicineId; set { _medicineId = value; } }
        [JsonProperty("PrescriptionID")]
        public int PrescriptionID { get => _prescriptionId; set { _prescriptionId = value; } }

        public PrescriptionMedicine()
        {

        }

        public PrescriptionMedicine(int medicineId, int prescriptionId)
        {
            _medicineId = medicineId;
            _prescriptionId = prescriptionId;
        }
    }
}
