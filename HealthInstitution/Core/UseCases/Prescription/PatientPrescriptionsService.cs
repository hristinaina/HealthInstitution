using HealthInstitution.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Services
{
    class PatientPrescriptionsService : IPatientPrescriptionService
    {
        public List<Prescription> _prescriptions;
        public List<Examination> _examinations;

        public PatientPrescriptionsService(Patient patient)
        {
            _examinations = patient.Examinations;
            _prescriptions = GetUpgoingPrescriptions();
        }

        public List<Prescription> GetUpgoingPrescriptions()
        {

            List<Prescription> upgoingPrescriptions = new();
            foreach (Examination examination in _examinations)
            {
                foreach (Prescription prescription in examination.Prescriptions)
                {
                    if (IsUpgoing(examination, prescription))
                    {
                        upgoingPrescriptions.Add(prescription);
                    }
                }
            }
            return upgoingPrescriptions;
        }

        public List<Prescription> GetAllPrescriptions()
        {
            List<Prescription> prescriptions = new();
            foreach (Examination examination in _examinations)
            {
                foreach (Prescription prescription in examination.Prescriptions)
                {
                    prescriptions.Add(prescription);
                }
            }
            return prescriptions;

        }

        public DateTime GetPrescriptionDate(Prescription p)
        {
            foreach (Examination examination in _examinations)
            {
                foreach (Prescription prescription in examination.Prescriptions)
                {
                    if (p.ID == prescription.ID)
                    {
                        return examination.Date;
                    }
                }
            }
            return DateTime.Now;
        }

        private bool IsUpgoing(Examination examination, Prescription prescription)
        {
            return examination.Date + new TimeSpan(prescription.LongitudeInDays, 0, 0, 0) > DateTime.Now && examination.Date < DateTime.Now;
        }
    }
}
