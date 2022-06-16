using System.Collections.Generic;

namespace HealthInstitution.Core.Services.Equipments
{
    public interface ISearchEquipmentService
    {
        public Dictionary<Equipment, List<Room>> Search(string phrase);
    }
}