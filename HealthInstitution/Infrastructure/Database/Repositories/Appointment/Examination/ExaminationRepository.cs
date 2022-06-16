using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core
{
    public class ExaminationRepository : BaseRepository, IExaminationRepository
    {
        private List<Examination> _examinations;

        public List<Examination> Examinations { get => _examinations; }
        public ExaminationRepository(string filePath)
        {
            _fileName = filePath;
            _examinations = new List<Examination>();
        }

        public override void LoadFromFile()
        {
            _examinations = FileService.Deserialize<Examination>(_fileName);

        }

        public override void SaveToFile()
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
                if (examination.Patient is not null && examination.Patient.ID == patientId)
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
                    i.Prescriptions = examination.Prescriptions;
                    i.Room = examination.Room;
                    return true;
                }
            }
            return false;
        }
        
        public int GetNewID()
        {
            if (_examinations.Count == 0)
            {
                return 1;
            }
            return _examinations.Max(x => x.ID) + 1;
        }

        public void Add(Examination examination)
        {
            _examinations.Add(examination);
        }

        public void Remove(Examination examination)
        {
            _examinations.Remove(examination);
        }

        public void DeleteByPatientID(int patientId)
        {
            foreach (Examination examination in _examinations)
            {
                if (examination.Patient.ID == patientId)
                {
                    _examinations.Remove(examination);
                }
            }
        }

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate)
        {
            foreach (Examination appointment in Examinations)
            {
                if (appointment.Date == oldDate && appointment.Doctor == doctor && appointment.Patient == patient)
                {
                    return appointment;
                }
            }
            return null;
        }

        public List<Examination> GetExaminations()
        {
            return _examinations;
        }
    }
}