using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class DayOffRepository : BaseRepository, IDayOffRepository
    {
        private List<DayOff> _daysOff;

        public List<DayOff> DaysOff { get => _daysOff; }
        public DayOffRepository(string filePath)
        {
            _fileName = filePath;
            _daysOff = new List<DayOff>();
        }

        public override void LoadFromFile()
        {
            _daysOff = FileService.Deserialize<DayOff>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<DayOff>(_fileName, _daysOff);
        }
        public DayOff FindByID(int id)
        {
            foreach (DayOff dayOff in _daysOff)
            {
                if (dayOff.ID == id) return dayOff;
            }
            return null;
        }

        public int FindNewID()
        {
            if (_daysOff.Count == 0)
            {
                return 1;
            }
            return _daysOff.Max(x => x.ID) + 1;
        }

        public List<DayOff> DoctorDaysOffToDaysOff(List<DoctorDaysOff> doctorDaysOff)
        {
            List<DayOff> daysOff = new();
            foreach (DoctorDaysOff reference in doctorDaysOff)
            {
                daysOff.Add(FindByID(reference.DaysOffId));

            }
            return daysOff;
        }
    }
}
