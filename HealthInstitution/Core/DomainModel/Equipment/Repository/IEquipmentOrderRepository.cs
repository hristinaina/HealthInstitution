using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public interface IEquipmentOrderRepository
    {
        public EquipmentOrder FindById(int id);

        public int GetNewID();

        public void CreateOrder(Equipment equipment, int quantity);

        public void Deliver(IEquipmentRepositoryService equipments);

        public string CheckIfOrdered(Equipment equipment);

        public List<EquipmentOrder> GetOrders();
    }
}