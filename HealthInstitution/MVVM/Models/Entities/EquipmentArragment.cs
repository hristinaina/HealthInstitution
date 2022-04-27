using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
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

        public static List<EquipmentArragment> LoadData(string filename)
        {
            List<EquipmentArragment> allArragments = FileService.Deserialize<EquipmentArragment>(filename);
            //rethink about name
            List<EquipmentArragment> activeArragments = new List<EquipmentArragment>();

            foreach (EquipmentArragment a in allArragments)
            {
                if (a.EndDate > DateTime.Today) activeArragments.Add(a);
            }
            return activeArragments;
        }

        public static List<EquipmentArragment> GetCurrentArragments(List<EquipmentArragment> arragments)
        {
            List<EquipmentArragment> currentArragments = new List<EquipmentArragment>();
            foreach (EquipmentArragment a in arragments)
            {
                if (a.StartDate < DateTime.Today && a.EndDate > DateTime.Today) currentArragments.Add(a);
            }
            return currentArragments;
        }

        public static void ArrangeEquipment(List<Room> rooms, List<Equipment> equipment, List<EquipmentArragment> arragments)
        {
            foreach (EquipmentArragment a in arragments)
            {
                Room r = Room.GetById(rooms, a.RoomId);
                Equipment e = Equipment.GetById(equipment, a.EquipmentId);
                r.AddEquipment(e, a.Quantity);
                e.ArrangeInRoom(r, a.Quantity);
            }
        }
    }
}
