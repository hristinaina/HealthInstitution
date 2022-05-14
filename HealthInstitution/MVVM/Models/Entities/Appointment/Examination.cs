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
        private List<Prescription> _prescriptions;
        private ExaminationReview _review;

        [JsonProperty("Anamnesis")]
        public string Anamnesis { get => _anamnesis; set { _anamnesis = value; } }
        [JsonProperty("Review")]
        public ExaminationReview Review { get => _review; set { _review = value; } }
        [JsonIgnore]
        public List<Prescription> Prescriptions { get => _prescriptions; set { _prescriptions = value; } }

        public Examination()
        {
            _prescriptions = new List<Prescription>();
        }
        public Examination(int id, DateTime date, bool isEmergency, bool done,
                           string anamnesis, ExaminationReview review)
                           : base(id, date, isEmergency, done)
        {
            _anamnesis = anamnesis;
            _prescriptions = new List<Prescription>();
            _review = review;
        }


        public Examination(int id, Doctor doctor, Patient patient, DateTime date, List<Prescription> prescriptions)
        {
            ID = id;
            Doctor = doctor;
            Patient = patient;
            Date = date;
            Emergency = false;
            Prescriptions = prescriptions;
        }

        public void AddPrescription(Prescription prescription)
        {
            Prescriptions.Add(prescription);
            Institution.Instance().ExaminationRepository.Update(this);
        }

    }
}
