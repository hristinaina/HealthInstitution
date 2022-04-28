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
        public int ID { get => this._id; set { this._id = value; } }
        [JsonProperty("EquipmentID")]
        public int EquipmentID { get => this._equipmentId; set { this._equipmentId = value; } }
        [JsonProperty("DeliverDate")]
        public DateTime DeliverDate { get => this._deliverDate; set { this._deliverDate = value; } }
        [JsonProperty("Quantity")]
        public int Quantity { get => this._quantity; set { this._quantity = value; } }

        public EquipmentOrder()
        {

        }

        public bool isDelivered()
        {
            return this._deliverDate < DateTime.Today;
        }

    }
}
