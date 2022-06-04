﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Repositories;

namespace HealthInstitution.MVVM.Models.Services
{
    class ExaminationService
    {
        private ExaminationRepository _examinationRepository;

        public ExaminationService()
        {
            _examinationRepository = Institution.Instance().ExaminationRepository;
        }
        public void AddPrescription(Examination examination, Prescription prescription)
        {
            examination.Prescriptions.Add(prescription);
            //Institution.Instance().ExaminationRepository.Update(examination);
        }

        public bool AddAnamnesis(Examination examination, string anamnesis)
        {
            foreach (Examination i in _examinationRepository.Examinations)
            {
                if (i.ID == examination.ID)
                {
                    examination.Anamnesis = anamnesis;
                    return true;
                }
            }
            return false;
        }

    }
}
