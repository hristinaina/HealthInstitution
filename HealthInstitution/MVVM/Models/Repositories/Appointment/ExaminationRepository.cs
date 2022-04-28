using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models
{
    public class ExaminationRepository
    {
        private string _fileName;
        private List<Examination> _examinations;

        public List<Examination> Examinations { get => _examinations; }
        public ExaminationRepository(string filePath)
        {
            _fileName = filePath;
            _examinations = new List<Examination>();
        }

        public void LoadFromFile()
        {
            _examinations = FileService.Deserialize<Examination>(_fileName);

        }

        public void SaveToFile()
        {
            FileService.Serialize<Examination>(_fileName, _examinations);
        }
        public Examination FindByID(int id)
        {
            foreach (Examination examination in _examinations)
            {
                if (examination.ID == id) return examination;
            }
            return null;
        }

        public List<Examination> FindByPatientID(int patientId)
        {
            List<Examination> examinations = new List<Examination>();
            foreach (Examination examination in _examinations)
            {
                if (examination.Patient.ID == patientId)
                    examinations.Add(examination);
            }
            return examinations;
        }

        public bool Update(Examination examination)
        {
            foreach(Examination i in _examinations)
            {
                if (i.ID == examination.ID)
                {
                    i.Date = examination.Date;
                    i.Emergency = examination.Emergency;
                    i.Done = examination.Done;
                    i.Anamnesis = examination.Anamnesis;
                    i.Perscription = examination.Perscription;
                    i.Room = examination.Room;
                    return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            foreach(Examination examination in _examinations)
            {
                if (examination.ID == id)
                {
                    _examinations.Remove(examination);
                    return true;
                }
            }
            return false;

        }
    }
}