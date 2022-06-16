using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IEquipmentRepository : IRepository
    {
        public Equipment FindById(int id);

        public List<Equipment> GetEquipment();
    }
}