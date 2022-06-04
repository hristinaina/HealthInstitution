﻿using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services.SearchingServices
{
    class DoctorsSearchService
    {
        DoctorRepository _repository;

        public DoctorsSearchService(DoctorRepository repository)
        {
            _repository = repository;
        }

        public List<Doctor> SearchForDoctor(Doctor doctor)
        {
            List<Doctor> doctors = new List<Doctor>();
            foreach (Doctor d in _repository.Doctors)
            {
                bool add = false;
                if (doctor.FirstName != "" && d.FirstName.ToLower().Contains(doctor.FirstName.ToLower()))
                {
                    add = true;
                }
                if (doctor.LastName != "" && d.LastName.ToLower().Contains(doctor.LastName.ToLower()))
                {
                    add = true;
                }
                if (d.Specialization == doctor.Specialization)
                {
                    add = true;
                }
                if (add)
                {
                    doctors.Add(d);
                }
            }
            return doctors;
        }

    }
}