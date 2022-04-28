using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Services;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class DayOffRepository
    {
        private string _fileName;
        private List<DayOff> _daysOff;

        public List<DayOff> DaysOff { get => _daysOff; }
        public DayOffRepository(string filePath)
        {
            this._fileName = filePath;
            this._daysOff = new List<DayOff>();
        }

        public void LoadFromFile()
        {
            _daysOff = FileService.Deserialize<DayOff>(_fileName);
        }

        public void SaveToFile()
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
