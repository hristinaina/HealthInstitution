using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public class EquipmentOrderRepositoryService : IEquipmentOrderRepositoryService
    {
        private IEquipmentOrderRepository _repository;

        public EquipmentOrderRepositoryService()
        {
            _repository = Institution.Instance().EquipmentOrderRepository;
        }

        public EquipmentOrder FindById(int id)
        {
            return _repository.FindById(id);
        }

        public int GetNewID()
        {
            return _repository.GetNewID();
        }

        public void CreateOrder(Equipment equipment, int quantity)
        {
            _repository.CreateOrder(equipment, quantity);
        }

        public void Deliver(IEquipmentRepositoryService equipments)
        {
            _repository.Deliver(equipments);
        }

        public string CheckIfOrdered(Equipment equipment)
        {
            return _repository.CheckIfOrdered(equipment);
        }

        public List<EquipmentOrder> GetOrders()
        {
            return _repository.GetOrders();
        }
    }
}