using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class EquipmentOrder
    {
        private int _id;
        private int _equipmentId;
        private DateTime _deliverDate;
        private int _quantity;

        [JsonProperty("ID")]
        public int ID { get => _id; set => _id = value; }
        [JsonProperty("EquipmentID")]
        public int EquipmentID { get => _equipmentId; set => _equipmentId = value; }
        [JsonProperty("DeliverDate")]
        public DateTime DeliverDate { get => _deliverDate; set => _deliverDate = value; }
        [JsonProperty("Quantity")]
        public int Quantity { get => _quantity; set => _quantity = value; }

        public EquipmentOrder()
        {

        }

        public EquipmentOrder(int id, int equipmentId, DateTime deliverDate, int quantity)
        {
            _id = id;
            _equipmentId = equipmentId;
            _deliverDate = deliverDate;
            _quantity = quantity;
        }

        public bool isDelivered()
        {
            return _deliverDate < DateTime.Today;
        }

    }
}
