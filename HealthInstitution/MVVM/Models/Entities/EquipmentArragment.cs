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
        public int RoomId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EquipmentArragment()
        {

        }

        public static List<EquipmentArragment> LoadData(string filename)
        {
            List<EquipmentArragment> allArragments = FileService.Deserialize<EquipmentArragment>(filename);
            //promeni ime
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
    }
}
