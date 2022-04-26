using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class MedicalRecord
    {
        public double Height;
        public double Weight;
        // history of sickness !?
        public List<Allergen> Allergens;
        public List<Appointment> Appointments;

        public MedicalRecord()
        {
            Appointments = new List<Appointment>();
        }

    }
}
