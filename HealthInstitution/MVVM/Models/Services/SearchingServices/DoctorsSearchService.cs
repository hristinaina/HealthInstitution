using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Services.SearchingServices
{
    class DoctorsSearchService : SearchService
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
                if (isMatching(doctor.FirstName, d.FirstName))
                {
                    add = true;
                }
                if (isMatching(doctor.LastName, d.LastName))
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
