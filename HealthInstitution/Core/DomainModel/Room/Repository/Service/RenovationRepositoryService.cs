using System;

namespace HealthInstitution.Core.Repository
{
    public class RenovationRepositoryService : IRenovationRepositoryService
    {
        private IRenovationRepository _repository;

        public RenovationRepositoryService()
        {
            _repository = Institution.Instance().RenovationRepository;
        }


        public Renovation FindById(int id)
        {
            return _repository.FindById(id);
        }

        public Renovation Create(DateTime startDate, DateTime endDate)
        {
            return _repository.Create(startDate, endDate);
        }
    }
}