using System;
using System.Collections.Generic;
using System.Linq;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Repositories
{
    public class OperationRepository : BaseRepository, IOperationRepository
    {

        private List<Operation> _operations;

        public List<Operation> Operations { get => _operations; }
        public OperationRepository(string filePath)
        {
            _fileName = filePath;
            _operations = new List<Operation>();
        }

        public override void LoadFromFile()
        {
            _operations = FileService.Deserialize<Operation>(_fileName);
        }

        public override void SaveToFile()
        {
            FileService.Serialize<Operation>(_fileName, _operations);
        }
        public Operation FindByID(int id)
        {
            foreach (Operation operation in _operations)
            {
                if (operation.ID == id) return operation;
            }
            return null;
        }

        public List<Operation> FindByPatientID(int patientId)
        {
            List<Operation> operations = new();
            foreach (Operation operation in _operations)
            {
                if (operation.Patient.ID == patientId)
                    operations.Add(operation);
            }
            return operations;
        }

        public bool Update(Operation operation)
        {
            foreach (Operation i in _operations)
            {
                if (i.ID == operation.ID)
                {
                    i.Date = operation.Date;
                    i.Emergency = operation.Emergency;
                    i.Done = operation.Done;
                    i.Room = operation.Room;
                    return true;
                }
            }
            return false;
        }

        public void Remove(Operation operation)
        {
            _operations.Remove(operation);
        }

        public void Add(Operation operation)
        {
            _operations.Add(operation);
        }

        public int GetNewID()
        {
            if (_operations.Count == 0)
            {
                return 1;
            }
            return _operations.Max(x => x.ID) + 1;
        }

        public Appointment FindAppointment(Doctor doctor, Patient patient, DateTime oldDate)
        {
            foreach (Operation appointment in Operations)
            {
                if (appointment.Date == oldDate && appointment.Doctor == doctor && appointment.Patient == patient)
                {
                    return appointment;
                }
            }
            return null;
        }

        public List<Operation> GetOperations()
        {
            return _operations;
        }
    }
}
