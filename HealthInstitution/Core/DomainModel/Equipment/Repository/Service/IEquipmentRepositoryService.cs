namespace HealthInstitution.Core.Repository
{
    public interface IEquipmentRepositoryService
    {
        public Equipment FindByID(int id);
    }
}