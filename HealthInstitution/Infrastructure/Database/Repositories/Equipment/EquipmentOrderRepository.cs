using HealthInstitution.Core;
using HealthInstitution.Core.Services;
using System;
using System.Collections.Generic;

namespace HealthInstitution.Infrastructure.Database.Repositories
{
    public class EquipmentOrderRepository : BaseRepository
    {
        private List<EquipmentOrder> _orders;

        public List<EquipmentOrder> Rooms { get => _orders; }

        public EquipmentOrderRepository(string fileName)
        {
            _fileName = fileName;
            _orders = new List<EquipmentOrder>();
        }

        public override void LoadFromFile()
        {
            _orders = FileService.Deserialize<EquipmentOrder>(_fileName);
        }

        public override void SaveToFile()
        {

            FileService.Serialize<EquipmentOrder>(_fileName, _orders);
        }

        public EquipmentOrder FindById(int id)
        {
            foreach (EquipmentOrder o in _orders)
            {
                if (o.ID == id) return o;
            }
            return null;
        }

        private bool CheckID(int id)
        {
            foreach (EquipmentOrder e in _orders)
            {
                if (e.ID == id) return false;
            }
            return true;
        }

        public int GetNewID()
        {
            int i = 1;
            while (true)
            {
                if (CheckID(i)) return i;
                i++;
            }
        }

        public void CreateOrder(Equipment equipment, int quantity)
        {
            int id = GetNewID();
            DateTime deliverDate = DateTime.Now.AddDays(1);
            _orders.Add(new EquipmentOrder(id, equipment.ID, deliverDate, quantity));
        }

        public void Deliver(EquipmentRepository equipments)
        {
            List<EquipmentOrder> futureOrders = new List<EquipmentOrder>();
            //Room warehouse = rooms.FindById(0);

            foreach (EquipmentOrder o in _orders)
            {
                if (o.isDelivered())
                {
                    Equipment e = equipments.FindById(o.EquipmentID);
                    e.Quantity += o.Quantity;
                    //warehouse.AddEquipment(e, o.Quantity);
                }
                else futureOrders.Add(o);
            }
            _orders = futureOrders;
        }

        public string CheckIfOrdered(Equipment equipment)
        {
            string status = "Out of stock";
            int ordered = 0;

            foreach (EquipmentOrder order in _orders)
            {
                if (equipment.ID == order.EquipmentID && order.DeliverDate > DateTime.Now)
                {
                    ordered += order.Quantity;
                }
            }

            if (ordered != 0) status = "Ordered: " + ordered.ToString();

            return status;
        }
    }
}
