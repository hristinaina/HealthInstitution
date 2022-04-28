using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories.Room
{
    public class EquipmentOrderRepository
    {
        private string _fileName;
        private List<EquipmentOrder> _orders;

        public List<EquipmentOrder> Rooms { get => this._orders; }

        public EquipmentOrderRepository(string roomsFileName)
        {
            this._fileName = roomsFileName;
            this._orders = new List<EquipmentOrder>();
        }

        public void LoadFromFile()
        {
            this._orders = FileService.Deserialize<EquipmentOrder>(this._fileName);
        }

        public void SaveToFile()
        {

            FileService.Serialize<EquipmentOrder>(this._fileName, this._orders);
        }

        public EquipmentOrder FindById(int id)
        {
            foreach (EquipmentOrder o in this._orders)
            {
                if (o.ID == id) return o;
            }
            return null;
        }

        public void Deliver(EquipmentRepository equipments)
        {
            List<EquipmentOrder> futureOrders = new List<EquipmentOrder>();

            foreach (EquipmentOrder o in this._orders)
            {
                if (o.isDelivered())
                {
                    Equipment e = equipments.FindById(o.EquipmentID);
                    e.Quantity += o.Quantity;
                }
                else futureOrders.Add(o);
            }
            this._orders = futureOrders;
        }
    }
}
