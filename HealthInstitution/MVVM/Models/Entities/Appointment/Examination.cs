using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Examination : Appointment
    {
        private string _anamnesis;
        private Prescription _perscription;
        private ExaminationReview _review;
        private DateTime now;

        [JsonProperty("Anamnesis")]
        public string Anamnesis { get => _anamnesis; set { _anamnesis = value; } }
        [JsonProperty("Review")]
        public ExaminationReview Review { get => _review; set { _review = value; } }
        [JsonIgnore]
        public Prescription Perscription { get => _perscription; set { _perscription = value; } }

        public Examination()
        {
        }
        public Examination(int id, DateTime date, bool isEmergency, bool done,
                   string anamnesis, ExaminationReview review)
                  : base(id, date, isEmergency, done)
        {
            _anamnesis = anamnesis;
            _perscription = null;
            _review = review;
        }

        public Examination(Doctor doctor, DateTime time, Room room) : base(doctor, time, room)
        {
        }
    }
}
