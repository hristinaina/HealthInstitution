using System;

namespace HealthInstitution.Core.Repository
{
    public interface IRenovationRepositoryService
    {
        public Renovation FindById(int id);

        public Renovation Create(DateTime startDate, DateTime endDate);

    }
}