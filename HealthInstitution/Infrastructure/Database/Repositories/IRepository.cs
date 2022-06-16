namespace HealthInstitution.Core
{
    public interface IRepository
    {
        public void LoadFromFile();
        public void SaveToFile();
    }
}