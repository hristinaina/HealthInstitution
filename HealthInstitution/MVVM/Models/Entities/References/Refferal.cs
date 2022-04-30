using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HealthInstitution.MVVM.Models.Entities.References
{
    public class Refferal
    {
        private int _doctorId;
        private int _patientId;
        private Specialization _specialization;

        [JsonProperty("DoctorId")]
        public int DoctorId { get => _doctorId; set { _doctorId = value; } }
        [JsonProperty("PatientId")]
        public int PatientId { get => _patientId; set { _patientId = value; } }
        [JsonProperty("Specialization")]
        public Specialization Specialization { get => _specialization; set { _specialization = value; } }

        public Refferal()
        {

        }

        public Refferal(int patientId, int doctorId, Specialization specialization)
        {
            _patientId = patientId;
            _doctorId = doctorId;
            _specialization = specialization;
        }
    }
}
