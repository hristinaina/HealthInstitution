namespace HealthInstitution.Core.Repository
{
    public interface IEquipmentOrderRepository
    {
        public EquipmentOrder FindById(int id);

        public int GetNewID();

        public void CreateOrder(Equipment equipment, int quantity);

        public void Deliver(EquipmentRepository equipments);

        public string CheckIfOrdered(Equipment equipment)
    }
}