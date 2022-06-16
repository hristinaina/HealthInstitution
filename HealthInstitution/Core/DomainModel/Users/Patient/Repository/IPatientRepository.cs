namespace HealthInstitution.Core.Repository
{
    interface IPatientRepository
    {
        public Patient FindByID(int id);

        public int GetNewID();

        public void CreatePatient(string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight);
    }
}
