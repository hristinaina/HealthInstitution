using System.Collections.Generic;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Repositories
{
    public class DoctorRepository : BaseRepository, IDoctorRepository
    {
        private List<Doctor> _doctors;

        public List<Doctor> Doctors { get => _doctors; }
        public DoctorRepository(string fileName)
        {
            _fileName = fileName;
            _doctors = new List<Doctor>();
        }

        public override void LoadFromFile()
        {
            _doctors = FileService.Deserialize<Doctor>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<Doctor>(_fileName, _doctors);
        }

        public Doctor FindByID(int id)
        {
            foreach(Doctor doctor in _doctors)
            {
                if (doctor.ID == id) return doctor;
            }
            return null;
        }

        public List<Doctor> GetGeneralPractitioners()
        {
            List<Doctor> generalPractitioners = new();

            foreach (Doctor doctor in _doctors)
            {
                if (doctor.Specialization == Specialization.NONE) generalPractitioners.Add(doctor);
            }

            return generalPractitioners;
        }

        public Doctor FindDoctorBySpecialization(Specialization specialization)
        {
            foreach (Doctor doctor in _doctors)
            {
                if (doctor.Specialization == specialization) return doctor;
            }
            return null;
        }
    }
}
