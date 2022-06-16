using System;
using System.Collections.Generic;

namespace HealthInstitution.Core
{
    public interface IDoctorRankingService
    {
        public List<Tuple<Doctor, double>> GetBest();

        public List<Tuple<Doctor, double>> GetWorst();
    }
}