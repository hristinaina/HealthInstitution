using HealthInstitution.Core;
using System.Collections.Generic;

namespace HealthInstitution.Services
{
    public interface IAnamnesisSearch
    {
        public List<Appointment> SearchByAnamnesis(string keyWord);
    }
}
