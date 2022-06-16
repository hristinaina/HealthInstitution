using HealthInstitution.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Core.Repositories
{
    public class ExaminationReferencesRepository : BaseRepository, IExaminationRelationsRepository
    {
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

        public override void LoadFromFile()
        {
            _references = FileService.Deserialize<ExaminationReference>(_fileName);
        }

        public override void SaveToFile()
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

        public void Add(Examination examination)
        {
            foreach (Prescription prescription in examination.Prescriptions)
            _references.Add(new ExaminationReference(examination.ID, examination.Doctor.ID, examination.Patient.ID, examination.Room.ID, prescription.ID));

            if (examination.Prescriptions.Count() == 0) 
                _references.Add(new ExaminationReference(examination.ID, examination.Doctor.ID, examination.Patient.ID, examination.Room.ID, -1));
        }

        public void Add(ExaminationReference examinationReference)
        {
            _references.Add(examinationReference);
        }

        public void Remove(Examination examination)
        {
            ExaminationReference reference = FindByExaminationID(examination.ID);
            _references.Remove(reference);
        }
    }
}
