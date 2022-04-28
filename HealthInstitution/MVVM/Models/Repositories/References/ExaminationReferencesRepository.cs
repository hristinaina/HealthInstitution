using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class ExaminationReferencesRepository
    {
        private string _fileName;
        private List<ExaminationReference> _references;

        public ExaminationReferencesRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<ExaminationReference>();
        }
        public List<ExaminationReference> GetReferences()
        {
            return _references;
        }

        public void LoadFromFile()
        {
            _references = FileService.Deserialize<ExaminationReference>(_fileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<ExaminationReference>(_fileName, _references);
        }

        public ExaminationReference FindByExaminationID(int id)
        {
            foreach (ExaminationReference reference in _references)
            {
                if (reference.ExaminationID == id) return reference;
            }
            return null;
        }
    }
}
