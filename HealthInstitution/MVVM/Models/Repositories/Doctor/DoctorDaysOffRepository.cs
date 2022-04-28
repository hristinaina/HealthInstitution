using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class DoctorDaysOffRepository
    {
        private string _fileName;
        private List<DoctorDaysOff> _references;

        public DoctorDaysOffRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<DoctorDaysOff>();
        }
        public List<DoctorDaysOff> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<DoctorDaysOff>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<DoctorDaysOff>(_fileName, _references);
        }

        public List<DoctorDaysOff> FindByDoctorID(int doctorId)
        {
            List<DoctorDaysOff> doctorsDaysOff = new();
            foreach (DoctorDaysOff reference in _references)
            {
                if (reference.DoctorID == doctorId)
                    doctorsDaysOff.Add(reference);
            }
            return doctorsDaysOff;
        }
    }
}
