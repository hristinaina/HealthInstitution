using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Enumerations;
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
        private readonly string _fileName;
        private List<ExaminationChange> _references;

        public List<ExaminationChange> Changes { get => _references; }
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

        public void Add(Examination examination, bool resolved, AppointmentStatus status)
        {
            ExaminationChange change = new ExaminationChange(examination.Patient.ID, examination.ID, status, DateTime.Now, resolved);
            _references.Add(change);
            examination.Patient.ExaminationChanges.Add(change);
            
        public ExaminationChange FindByID(int id)
        {
            foreach (ExaminationChange reference in _references)
            {
                if (reference.ID == id) return reference;
            }
            return null;
        }
    }
}
