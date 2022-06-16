namespace HealthInstitution.Core
{
    public abstract class BaseRepository : IRepository
    {
        protected string _fileName;

        public abstract void LoadFromFile();
        public abstract void SaveToFile();
    }
}