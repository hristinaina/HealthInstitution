using HealthInstitution.Core;
using HealthInstitution.Repositories;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public class DoctorsSearchService : SearchService, IDoctorSearch
    {
        private readonly DoctorRepository _repository;
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
                if (IsMatching(doctor.FirstName, d.FirstName))
                {
                    add = true;
                }
                if (IsMatching(doctor.LastName, d.LastName))
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
