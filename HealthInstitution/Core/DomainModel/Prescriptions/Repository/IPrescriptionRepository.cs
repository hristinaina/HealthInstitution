namespace HealthInstitution.Core.Repository
{
    public interface IPrescriptionRepository
    {
        public Prescription FindByID(int id);

        public int GetID();

        public void Add(Prescription prescription);

    }
}