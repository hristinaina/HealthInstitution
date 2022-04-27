using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.Repositories
{
    public class DoctorRepository
    {
        private string _doctorFileName;
        private List<Doctor> _doctors;

        public DoctorRepository(string doctorFileName)
        {
            this._doctorFileName = doctorFileName;
            this._doctors = new List<Doctor>();
        }

        public List<Doctor> GetDoctors()
        {
            return this._doctors;
        }

        public void LoadFromFile()
        {
            _doctors = FileService.Deserialize<Doctor>(_doctorFileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Doctor>(_doctorFileName, _doctors);
        }
    }
}
