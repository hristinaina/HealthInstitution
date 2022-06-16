using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.UseCases
{
    public interface IExaminationChangeService
    {
        public string ApproveChange(int requestId);

        public void RejectChange(int requestId);

        public void RemoveOutdatedRequests();
    }
}
