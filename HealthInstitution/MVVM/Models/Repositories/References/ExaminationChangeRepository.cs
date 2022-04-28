using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories.References
{
    public class ExaminationChangeRepository
    {
        private string _fileName;
        private List<ExaminationChange> _references;

        public List<ExaminationChange> Changes { get => _references;}
        public ExaminationChangeRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<ExaminationChange>();
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<ExaminationChange>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<ExaminationChange>(_fileName, _references);
        }

        public ExaminationChange FindByAppointmentID(int id)
        {
            foreach (ExaminationChange reference in _references)
            {
                if (reference.AppointmentID == id) return reference;
            }
            return null;
        }
    }
}
