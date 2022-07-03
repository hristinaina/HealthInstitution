using System.Linq;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services
{
    class StartRenovationService : IStartRenovationService
    {

        public StartRenovationService()
        {
        }

        public void StartRenovation(Renovation renovation)
        {
            foreach (Room r in renovation.RoomsUnderRenovation) r.UnderRenovation = true;
            renovation.Started = true;

            if (renovation.RoomsUnderRenovation.Count() > 1 || renovation.Result.Count() > 1)
            {
                IRoomRepositoryService roomManager = new RoomRepositoryService();
                foreach (Room r in renovation.RoomsUnderRenovation)
                {
                    roomManager.ReturnEquipmentToWarehouse(renovation.EndDate, r);
                }
            }

        }
    }
}
