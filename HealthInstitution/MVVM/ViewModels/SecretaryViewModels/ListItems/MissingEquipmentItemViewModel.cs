using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.ViewModels.SecretaryViewModels.ListItems
{
    public class MissingEquipmentItemViewModel : BaseViewModel
    {
        Equipment _equipment;
        string _status;

        public string Id => _equipment.ID.ToString();
        public string Name => _equipment.Name;
        public string Type => _equipment.Type.ToString();
        public string Status => _status;

        public MissingEquipmentItemViewModel(Equipment equipment, string status)
        {
            _equipment = equipment;
            _status = status;
        }
    }
}
