using System.Collections.Generic;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models.Entities;

namespace HealthInstitution.MVVM.Models.Services.Equipments
{
    public class SearchEquipmentService
    {
        private EquipmentRepository _equipment;

        public SearchEquipmentService()
        {
            _equipment = Institution.Instance().EquipmentRepository;
        }

        public Dictionary<Equipment, List<Room>> Search(string phrase)
        {
            if (phrase is null || phrase.Equals(""))
                throw new EmptySearchPhraseException("You need to enter phrase for search");

            Dictionary<Equipment, List<Room>> matchingEquipment = new Dictionary<Equipment, List<Room>>();

            foreach (Equipment e in _equipment.Equipment)
            {
                if (e.Name.ToLower().Contains(phrase.ToLower()))
                {
                    matchingEquipment.Add(e, new List<Room>());
                    foreach (Room r in e.ArrangmentByRooms.Keys) matchingEquipment[e].Add(r);
                }
                else
                {
                    SearchInRoomName(e, phrase, matchingEquipment);
                }
            }

            return matchingEquipment;
        }

        private void SearchInRoomName(Equipment equipment, string phrase, Dictionary<Equipment, List<Room>> matchingEquipment)
        {
            foreach (Room r in equipment.ArrangmentByRooms.Keys)
            {
                if (r.Name.ToLower().Contains(phrase.ToLower()))
                {
                    if (!matchingEquipment.ContainsKey(equipment)) matchingEquipment.Add(equipment, new List<Room>());
                    matchingEquipment[equipment].Add(r);
                }
            }
        }
    }
}