using HealthInstitution.MVVM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services
{
    class ExaminationPrescriptionsService
    {
        public List<Prescription> _prescriptions;
        public Examination _examination;

        public ExaminationPrescriptionsService(Examination examination)
        {
            _examination = examination;
            _prescriptions = examination.Prescriptions;
        }

        public List<Prescription> GetUpgoingPrescriptions()
        {

            List<Prescription> upgoingPrescriptions = new();
            foreach (Prescription prescription in _prescriptions)
            {
                if (isUpgoing(prescription)) {
                    upgoingPrescriptions.Add(prescription);
                }
            }
            return upgoingPrescriptions;
        }

        private bool isUpgoing(Prescription prescription)
        {
            return _examination.Date + new TimeSpan(prescription.LongitudeInDays, 0, 0) < DateTime.Now;
        }
    }
}
