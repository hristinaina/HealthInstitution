﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.Core;
using HealthInstitution.Core.Services;

namespace HealthInstitution.Core.Services.Renovations
{
    class StartRenovationService
    {
        private Renovation _renovation;

        public StartRenovationService(Renovation r)
        {
            _renovation = r;
        }

        public void StartRenovation()
        {
            foreach (Room r in _renovation.RoomsUnderRenovation) r.UnderRenovation = true;
            _renovation.Started = true;

            if (_renovation.RoomsUnderRenovation.Count() > 1 || _renovation.Result.Count() > 1)
            {
                RoomService room = new RoomService();
                foreach (Room r in _renovation.RoomsUnderRenovation)
                {
                    room.ReturnEquipmentToWarehouse(_renovation.EndDate, r);
                }
            }

        }
    }
}