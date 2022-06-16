using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Core.Repository
{
    public interface IPrescriptionRepositoryService
    {
        public Prescription FindByID(int id);

        public int GetNewID();

        public void Add(Prescription prescription);

        public List<Prescription> GetPrescriptions();
    }
}
