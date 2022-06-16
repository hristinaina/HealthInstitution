using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HealthInstitution.Core
{
    public class Referral
    {
        private int _id;
        private int _doctorId;
        private int _patientId;
        private Specialization _specialization;

        [JsonProperty("ID")]
        public int Id { get => _id; set { _id = value; } }
        [JsonProperty("DoctorId")]
        public int DoctorId { get => _doctorId; set { _doctorId = value; } }
        [JsonProperty("PatientId")]
        public int PatientId { get => _patientId; set { _patientId = value; } }
        [JsonProperty("Specialization")]
        public Specialization Specialization { get => _specialization; set { _specialization = value; } }

        public Referral()
        {

        }

        public Referral(int id, int patientId, int doctorId, Specialization specialization)
        {
            _id = id;
            _patientId = patientId;
            _doctorId = doctorId;
            _specialization = specialization;
        }
    }
}
