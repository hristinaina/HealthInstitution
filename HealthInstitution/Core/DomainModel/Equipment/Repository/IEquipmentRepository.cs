using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IEquipmentRepository
    {
        public Equipment FindById(int id);

        public List<Equipment> GetEquipment();
    }
}