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
        private Perscription _perscription;
        private ExaminationReview _review;

        [JsonProperty("Anamnesis")]
        public string Anamnesis { get => _anamnesis; set { _anamnesis = value; } }
        [JsonProperty("Review")]
        public ExaminationReview Review { get => _review; set { _review = value; } }

        public Examination(int id, DateTime date, bool isEmergency, bool done,
                            string anamnesis, ExaminationReview review)
                           : base(id, date, isEmergency, done)
        {
            _anamnesis = anamnesis;
            _perscription = null;
            _review = review;
        }

        public string GetAnamnesis() => _anamnesis;
        public void SetAnamnesis() => _anamnesis = _anamnesis;
        public Perscription GetPerscription() => _perscription;
        public void SetPerscription(Perscription perscription) => _perscription = perscription;
        public ExaminationReview GetReview() => _review;
        public void SetReview(ExaminationReview review) => _review = review;
    }
}
