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

        private string _operationFileName;
        private List<Operation> _operations;

        public OperationRepository(string operationFilePath)
        {
            _operationFileName = operationFilePath;
            _operations = new List<Operation>();
        }

        public List<Operation> GetOperations()
        {
            return _operations;
        }

        public void LoadFromFile()
        {
            _operations = FileService.Deserialize<Operation>(_operationFileName);
        }

        public void SaveToFile()
        {
            FileService.Serialize<Operation>(_operationFileName, _operations);
        }
        public Operation FindByID(int id)
        {
            foreach (Operation operation in _operations)
            {
                if (operation.GetId() == id) return operation;
            }
            return null;
        }
    }
}
