using HealthInstitution.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Repository;

namespace HealthInstitution.Core.Services.Renovations
{
    class RenovationService
    {
        private IRenovationRepositoryService _renovations;
        private IRoomRenovationRepositoryService _roomsUnderRenovation;
        private readonly EndRenovationService _endRenovationService;

        public RenovationService()
        {
            _renovations = new RenovationRepositoryService();
            _roomsUnderRenovation = new RoomRenovationRepositoryService();
            _endRenovationService = new EndRenovationService();
        }



        public void StartRenovations()
        {
            foreach (Renovation r in _renovations.GetRenovations())
            {
                if (r.IsStarted())
                {
                    StartRenovationService service = new StartRenovationService(r);
                    service.StartRenovation();
                }
            }
        }

        public void EndRenovations()
        {
            List<int> endedRenovations = new List<int>();
            foreach (Renovation r in _renovations.GetRenovations())
            {
                if (r.EndDate <= DateTime.Today) endedRenovations.Add(r.ID);
            }

            foreach (int renovationId in endedRenovations)
            {
                EndRenovation(renovationId);
            }
        }

        private void EndRenovation(int id)
        {
            Renovation r = _renovations.FindById(id);
            
            _endRenovationService.EndRenovation(r);

            List<RoomRenovation> notCompletedRenovations = new List<RoomRenovation>();
            foreach (RoomRenovation roomUnderRenovation in _roomsUnderRenovation.GetRooms())
            {
                if (r.ID != roomUnderRenovation.RenovationId)
                {
                    notCompletedRenovations.Add(roomUnderRenovation);
                }
            }

            _roomsUnderRenovation.SetRooms(notCompletedRenovations);

            _renovations.GetRenovations().Remove(r);
        }
    }
}
