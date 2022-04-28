using HealthInstitution.MVVM.Models.Entities;
using HealthInstitution.MVVM.Models.Entities.References;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Repositories.Room
{
    public class EquipmentArragmentRepository
    {
        private readonly string _fileName;
        private List<EquipmentArragment> _arragments;

        public List<EquipmentArragment> Equipment { get => _arragments; }

        public EquipmentArragmentRepository(string roomsFileName)
        {
            _fileName = roomsFileName;
            _arragments = new List<EquipmentArragment>();
        }
        public void LoadFromFile(string filename)
        {
            List<EquipmentArragment> allArragments = FileService.Deserialize<EquipmentArragment>(filename);
            //rethink about name
            if (_arragments is null) _arragments = new List<EquipmentArragment>();

            foreach (EquipmentArragment a in allArragments)
            {
                if (a.EndDate > DateTime.Today) _arragments.Add(a);
            }
        }

        public void SaveToFile()
        {
            FileService.Serialize<EquipmentArragment>(_fileName, _arragments);
        }

        public List<EquipmentArragment> GetCurrentArragments()
        {
            List<EquipmentArragment> currentArragments = new List<EquipmentArragment>();
            foreach (EquipmentArragment a in _arragments)
            {
                if (a.StartDate < DateTime.Today && a.EndDate > DateTime.Today) currentArragments.Add(a);
            }
            return currentArragments;
        }
    }
}
