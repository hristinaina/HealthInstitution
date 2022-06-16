using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IEquipmentRepositoryService
    {
        public Equipment FindByID(int id);

        public List<Equipment> GetEquipment();
    }
}