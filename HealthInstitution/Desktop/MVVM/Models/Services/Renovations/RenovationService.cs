using HealthInstitution.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;

namespace HealthInstitution.Core.Services.Renovations
{
    class RenovationService
    {
        private RenovationRepository _renovations;
        private RoomRenovationRepository _roomsUnderRenovation;

        public RenovationService()
        {
            _renovations = Institution.Instance().RenovationRepository;
            _roomsUnderRenovation = Institution.Instance().RoomRenovationRepository;
        }



        public void StartRenovations()
        {
            foreach (Renovation r in _renovations.Renovations)
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
            foreach (Renovation r in _renovations.Renovations)
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
            EndRenovationService service = new EndRenovationService(r);
            service.EndRenovation();

            List<RoomRenovation> notCompletedRenovations = new List<RoomRenovation>();
            foreach (RoomRenovation roomUnderRenovation in _roomsUnderRenovation.RoomsUnderRenovations)
            {
                if (r.ID != roomUnderRenovation.RenovationId)
                {
                    notCompletedRenovations.Add(roomUnderRenovation);
                }
            }
            _roomsUnderRenovation.RoomsUnderRenovations = notCompletedRenovations;

            _renovations.Renovations.Remove(r);
        }
    }
}
