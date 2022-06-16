using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository { 
    public interface IOperationRelationsRepositoryService
    {
        public List<OperationReference> GetReferences();

        public OperationReference FindByOperationID(int id);

        public void Remove(Operation operation);

        public void Add(Operation operation);
    }
}
