using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class OperationReference
    {
        private int _operationId;
        private int _doctorId;
        private int _patientId;
        private int _roomId;

        [JsonProperty("OperationId")]
        public int OperationId { get => _operationId; set { _operationId = value; } }
        [JsonProperty("DoctorID")]
        public int DoctorID { get => _doctorId; set { _doctorId = value; } }
        [JsonProperty("PatientID")]
        public int PatientID { get => _patientId; set { _patientId = value; } }
        [JsonProperty("RoomID")]
        public int RoomID { get => _roomId; set { _roomId = value; } }

        public OperationReference(int operationId, int doctoriId, int patientId, int roomId)
        {
            _operationId = operationId;
            _doctorId = doctoriId;
            _patientId = patientId;
            _roomId = roomId;
        }
    }
}
