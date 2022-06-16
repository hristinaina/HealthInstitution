using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IOperationRelationsRepository
    {
        public List<OperationReference> GetReferences();

        public OperationReference FindByOperationID(int id);

        public void Remove(Operation operation);

        public void Add(Operation operation);
    }
}