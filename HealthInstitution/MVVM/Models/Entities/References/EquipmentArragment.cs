﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities.References
{
    public class EquipmentArragment
    {
        private int _roomId;
        private int _equipmentId;
        private int _quantity;
        private DateTime _startDate;
        private DateTime _endDate;
        public int RoomId { get => this._roomId; set { this._roomId = value; } }
        public int EquipmentId { get => this._equipmentId; set { this._equipmentId = value; } }
        public int Quantity { get => this._quantity; set { this._quantity = value; } }
        public DateTime StartDate { get => this._startDate; set { this._startDate = value; } }
        public DateTime EndDate { get => this._endDate; set { this._endDate = value; } }
        public EquipmentArragment()
        {

        }
    }
}
