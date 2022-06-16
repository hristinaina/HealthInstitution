using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthInstitution.Core.Repositories.References
{
    public class ExaminationChangeRepository : BaseRepository, IExaminationChangeRepository
    {
        private List<ExaminationChange> _changes;

        public List<ExaminationChange> Changes { get => _changes; }
        public ExaminationChangeRepository(string FileName)
        {
            _fileName = FileName;
            _changes = new List<ExaminationChange>();
        }

        public override void LoadFromFile()
        {
            _changes = FileService.Deserialize<ExaminationChange>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<ExaminationChange>(_fileName, _changes);
        }

        public ExaminationChange FindByAppointmentID(int id)
        {
            foreach (ExaminationChange reference in _changes)
            {
                if (reference.AppointmentID == id) return reference;
            }
            return null;
        }
        public int GetNewID()
        {
            if (_changes.Count == 0)
            {
                return 1;
            }
            return _changes.Max(x => x.ID) + 1;
        }

        public void Add(Examination examination, DateTime dateTime, bool resolved, AppointmentStatus status)
        {
            ExaminationChange change = new ExaminationChange(GetNewID(), examination.Patient.ID, examination.ID, status, DateTime.Now, resolved, dateTime);
            _changes.Add(change);
            examination.Patient.ExaminationChanges.Add(change);
        }

        public ExaminationChange FindByID(int id)
        {
            foreach (ExaminationChange reference in _changes)
            {
                if (reference.ID == id) return reference;
            }
            return null;
        }


        public void DeleteUnresolvedRequestsByPatientId(int patientId)
        {
            foreach (ExaminationChange reference in _changes)
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
            foreach (ExaminationChange e in _changes)
            {
                if (e.ID == id) return false;
            }
            return true;
        }

        public void RemoveByAppointmentId(int appointmentId)
        {
            List<ExaminationChange> requests = _changes.ToList();
            foreach (ExaminationChange reference in requests)
            {
                if (reference.AppointmentID == appointmentId) _changes.Remove(reference);
            }
        }

        public List<ExaminationChange> GetChanges()
        {
            return _changes;
        }
    }
}
