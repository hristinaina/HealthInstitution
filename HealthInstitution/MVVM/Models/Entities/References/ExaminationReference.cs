using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class ExaminationReference
    {
        private int _examinationID;
        private int _doctorId;
        private int _patientId;
        private int _roomId;
        private int _perscriptionId;

        [JsonProperty("ExaminationID")]
        public int ExaminationID { get => _examinationID; set { _examinationID = value; } }
        [JsonProperty ("DoctorID")]
        public int DoctorID { get => _doctorId; set { _doctorId = value; } }
        [JsonProperty("PatientID")]
        public int PatientID { get => _patientId; set { _patientId = value; } }
        [JsonProperty("RoomID")]
        public int RoomID { get => _roomId; set { _roomId = value; } }
        [JsonProperty("PerscriptionID")]
        public int PerscriptionID { get => _perscriptionId; set { _perscriptionId = value; } }



        public ExaminationReference(int examinationId, int doctoriId, int patientId,
                                     int roomId, int perscriptionId)
        {
            _examinationID = examinationId;
            _doctorId = doctoriId;
            _patientId = patientId;
            _roomId = roomId;
            _perscriptionId = perscriptionId;
        }
    }
}
