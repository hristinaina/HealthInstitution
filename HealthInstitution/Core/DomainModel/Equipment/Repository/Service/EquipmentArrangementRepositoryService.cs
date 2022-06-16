using System;
using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public class EquipmentArrangementRepositoryService : IEquipmentArrangementRepositoryService
    {
        private IEquipmentArrangementRepository _repository;

        public EquipmentArrangementRepositoryService()
        {
            _repository = Institution.Instance().EquipmentArragmentRepository;
        }


        public EquipmentArrangement FindCurrentArrangement(Room r, Equipment e)
        {
            return _repository.FindCurrentArrangement(r, e);
        }

        public EquipmentArrangement FindFirstBefore(Room r, Equipment e, DateTime date)
        {
            return _repository.FindFirstBefore(r, e, date);
        }

        public List<EquipmentArrangement> FindAllBefore(Room r, Equipment e, DateTime date)
        {
            return _repository.FindAllBefore(r, e, date);
        }

        public EquipmentArrangement FindFirstAfter(Room r, Equipment e, DateTime date)
        {
            return _repository.FindFirstAfter(r, e, date);
        }

        public List<EquipmentArrangement> FindAllAfter(Room r, Equipment e, DateTime date)
        {
            return _repository.FindAllAfter(r, e, date);
        }

        public List<EquipmentArrangement> GetArrangements()
        {
            return _repository.GetArrangements();
        }

        public List<EquipmentArrangement> GetCurrentArrangements()
        {
            return _repository.GetCurrentArrangements();
        }
    }
}