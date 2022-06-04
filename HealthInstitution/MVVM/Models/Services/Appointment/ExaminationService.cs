using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models.Services
{
    class ExaminationService
    {
        private Examination _examination;
        public Examination Examination { get => _examination; set { _examination = value; } }

        public ExaminationService(Examination examination)
        {
            _examination = examination;
        }
        public void AddPrescription(Prescription prescription)
        {
            _examination.Prescriptions.Add(prescription);
            Institution.Instance().ExaminationRepository.Update(_examination);
        }

    }
}
