using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models.Services.Renovations
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
                foreach (Room r in _renovation.RoomsUnderRenovation)
                {
                    r.ReturnEquipmentToWarehouse(_renovation.EndDate);
                }
            }

        }
    }
}
