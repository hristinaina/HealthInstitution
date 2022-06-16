using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public class OperationRelationsRepositoryService : IOperationRelationsRepositoryService
    {
        IOperationRelationsRepository _repository;

        public OperationRelationsRepositoryService()
        {
            _repository = Institution.Instance().OperationReferencesRepository;
        }

        public void Add(Operation operation)
        {
            _repository.Add(operation);
        }

        public void Remove(Operation operation)
        {
            _repository.Remove(operation);
        }

        public OperationReference FindByOperationID(int id)
        {
            return _repository.FindByOperationID(id);
        }

        public List<OperationReference> GetReferences()
        {
            return _repository.GetReferences();
        }

    }
}
