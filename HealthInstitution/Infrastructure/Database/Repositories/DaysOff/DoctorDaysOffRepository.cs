using System.Collections.Generic;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class DoctorDaysOffRepository : BaseRepository, IDoctorDaysOffRepository
    {
        private List<DoctorDaysOff> _references;

        public List<DoctorDaysOff> DoctorDaysOff { get => _references; }
        public DoctorDaysOffRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<DoctorDaysOff>();
        }

        public override void LoadFromFile()
        {
            _references = FileService.Deserialize<DoctorDaysOff>(_fileName);
        }

        public override void SaveToFile()
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

        public List<DoctorDaysOff> GetDoctorDaysOff()
        {
            return _references;
        }
    }
}
