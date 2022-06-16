using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IPrescriptionRepository : IRepository
    {
        public Prescription FindByID(int id);

        public int GetNewID();

        public void Add(Prescription prescription);

        public List<Prescription> GetPrescriptions();
    }
}