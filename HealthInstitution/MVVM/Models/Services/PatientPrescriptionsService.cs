using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    class PatientPrescriptionsService
    {
        public List<Prescription> _prescriptions;
        public List<Examination> _examination;

        public PatientPrescriptionsService(Patient patient)
        {
            _examination = patient.Examinations;
            _prescriptions = GetUpgoingPrescriptions();
        }

        public List<Prescription> GetUpgoingPrescriptions()
        {

            List<Prescription> upgoingPrescriptions = new();
            foreach (Examination examination in _examination) {
                foreach (Prescription prescription in examination.Prescriptions)
                {
                    if (isUpgoing(examination, prescription))
                    {
                        upgoingPrescriptions.Add(prescription);
                    }
                }
            }
            return upgoingPrescriptions;
        }

        private bool isUpgoing(Examination examination, Prescription prescription)
        {
            return examination.Date + new TimeSpan(prescription.LongitudeInDays, 0, 0, 0) > DateTime.Now;
        }
    }
}
