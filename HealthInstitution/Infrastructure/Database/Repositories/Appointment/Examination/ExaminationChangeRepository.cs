using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Core.Repositories.References
{
    public class ExaminationChangeRepository : BaseRepository, IExaminationChangeRepository
    {
        private List<ExaminationChange> _references;

        public List<ExaminationChange> Changes { get => _references; }
        public ExaminationChangeRepository(string FileName)
        {
            _fileName = FileName;
            _references = new List<ExaminationChange>();
        }

        public override void LoadFromFile()
        {
            _references = FileService.Deserialize<ExaminationChange>(_fileName);
        }

        public override void SaveToFile()
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
        public int GetNewID()
        {
            if (_references.Count == 0)
            {
                return 1;
            }
            return _references.Max(x => x.ID) + 1;
        }

        public void Add(Examination examination, DateTime dateTime, bool resolved, AppointmentStatus status)
        {
            ExaminationChange change = new ExaminationChange(GetNewID(), examination.Patient.ID, examination.ID, status, DateTime.Now, resolved, dateTime);
            _references.Add(change);
            examination.Patient.ExaminationChanges.Add(change);
        }

        public ExaminationChange FindByID(int id)
        {
            foreach (ExaminationChange reference in _references)
            {
                if (reference.ID == id) return reference;
            }
            return null;
        }


        public void DeleteUnresolvedRequestsByPatientId(int patientId)
        {
            foreach (ExaminationChange reference in _references)
            {
                if (!reference.Resolved && reference.PatientID == patientId)
                {
                    reference.ChangeStatus = AppointmentStatus.DELETED;
                    reference.Resolved = true;
                }
            }
        }

        private bool CheckID(int id)
        {
            foreach (ExaminationChange e in _references)
            {
                if (e.ID == id) return false;
            }
            return true;
        }

        public void RemoveByAppointmentId(int appointmentId)
        {
            List<ExaminationChange> requests = _references.ToList();
            foreach (ExaminationChange reference in requests)
            {
                if (reference.AppointmentID == appointmentId) _references.Remove(reference);
            }
        }
    }
}
