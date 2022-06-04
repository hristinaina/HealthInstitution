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

        public ExaminationService()
        {
           
        }
        public void AddPrescription(Examination examination, Prescription prescription)
        {
            examination.Prescriptions.Add(prescription);
            //Institution.Instance().ExaminationRepository.Update(examination);
        }

    }
}
