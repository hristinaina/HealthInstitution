using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public class Examination : Appointment
    {
        private string _anamnesis;
        private List<Prescription> _prescriptions;
        private Review _review;

        [JsonProperty("Anamnesis")]
        public string Anamnesis { get => _anamnesis; set { _anamnesis = value; } }
        [JsonProperty("Review")]
        public Review Review { get => _review; set { _review = value; } }
        [JsonIgnore]
        public List<Prescription> Prescriptions { get => _prescriptions; set { _prescriptions = value; } }

        public Examination()
        {
            _prescriptions = new List<Prescription>();
        }

        public Examination(Doctor doctor, Patient patient, DateTime datetime) : base(doctor, patient, datetime)
        {
        }

        public Examination(int id, DateTime date, bool isEmergency, bool done,
                           string anamnesis, Review review)
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

    }
}
