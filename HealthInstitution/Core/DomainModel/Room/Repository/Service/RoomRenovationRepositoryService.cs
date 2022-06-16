using System.Collections.Generic;

namespace HealthInstitution.Core.Repository
{
    public class RoomRenovationRepositoryService : IRoomRenovationRepositoryService
    {
        private IRoomRenovationRepository _repository;

        public RoomRenovationRepositoryService()
        {
            _repository = Institution.Instance().RoomRenovationRepository;
        }

        public List<RoomRenovation> GetRooms()
        {
            return _repository.GetRooms();
        }

        public void SetRooms(List<RoomRenovation> rooms)
        {
            _repository.SetRooms(rooms);
        }
    }
}