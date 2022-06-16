namespace HealthInstitution.Core.Repository
{
    public interface IPrescriptionRepository
    {
        public Prescription FindByID(int id);

        public int GetNewID();

        public void Add(Prescription prescription);

    }
}