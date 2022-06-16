using System;
using System.Collections.Generic;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.UseCases.RenovationScheduling;

namespace HealthInstitution.Core.Services
{
    class RenovationManagementService : IRenovationManagementService
    {
        private IRenovationRepositoryService _renovations;
        private IRoomRenovationRepositoryService _roomsUnderRenovation;
        private IEndRenovationService _endRenovationService;
        private IStartRenovationService _startRenovationService;

        public RenovationManagementService()
        {
            _renovations = new RenovationRepositoryService();
            _roomsUnderRenovation = new RoomRenovationRepositoryService();
            _endRenovationService = new EndRenovationService();
            _startRenovationService = new StartRenovationService();
        }



        public void StartRenovations()
        {
            foreach (Renovation r in _renovations.GetRenovations())
            {
                if (r.IsStarted())
                {
                    _startRenovationService.StartRenovation(r);
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
