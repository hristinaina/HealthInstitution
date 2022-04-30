using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;

namespace HealthInstitution.MVVM.Models.Repositories
{
    public class OperationRepository
    {

        private readonly string _fileName;
        private List<Operation> _operations;

        public List<Operation> Operations { get => _operations; }
        public OperationRepository(string filePath)
        {
            _fileName = filePath;
            _operations = new List<Operation>();
        }

        public void LoadFromFile()
        {
            _operations = FileService.Deserialize<Operation>(_fileName);
        }

        public void SaveToFile()
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

        public bool Delete(int id)
        {
            foreach(Operation i in _operations)
            {
                if (i.ID == id)
                {
                    _operations.Remove(i);
                    return true;
                }
            }
            return false;
        }

        public void DeleteByPatientID(int patientId)
        {
            foreach (Operation operation in _operations)
            {
                if (operation.Patient.ID == patientId)
                {
                    _operations.Remove(operation);
                }
            }
        }
    }
}
