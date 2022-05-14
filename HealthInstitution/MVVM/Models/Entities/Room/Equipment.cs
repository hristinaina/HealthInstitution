using HealthInstitution.Exceptions;
using HealthInstitution.Exceptions.AdminExceptions;
using HealthInstitution.MVVM.Models.Enumerations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Equipment
    {
        private int _id;
        private string _name;
        private int _quantity;
        private EquipmentType _type;
        private Dictionary<Room, int> _arrangmentByRooms;

        public int ID { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public EquipmentType Type { get => _type; set => _type = value; }
        [JsonIgnore]
        public Dictionary<Room, int> ArrangmentByRooms
        {
            get
            {
                if (_arrangmentByRooms is null) _arrangmentByRooms = new Dictionary<Room, int>();
                return _arrangmentByRooms;
            }
            set
            {
                _arrangmentByRooms = value;
            }
        }

        public Equipment()
        {
            _arrangmentByRooms = new Dictionary<Room, int>();
        }

        public Equipment(int id, string name, int quantity, EquipmentType type)
        {
            _id = id;
            _name = name;
            _quantity = quantity;
            _type = type;
        }

        public Equipment(int id, string name, int quantity, EquipmentType type, Dictionary<Room, int> arragment) : this(id, name, quantity, type)
        {
            _arrangmentByRooms = arragment;
        }

        public void ArrangeInRoom(Room r, int quantity)
        {
            if (!_arrangmentByRooms.ContainsKey(r))
            {
                _arrangmentByRooms[r] = 0;
            }
            _arrangmentByRooms[r] += quantity;
        }

        public int GetQuantityInRoom(Room r)
        {
            return _arrangmentByRooms[r];
        }

        public void ReturnToWarehouse(DateTime date, Room room)
        {
            Room warehouse = Institution.Instance().RoomRepository.FindById(0);

            EquipmentArrangement destinationRoomArrangement = Institution.Instance().EquipmentArragmentRepository.FindFirstBefore(room, this, date);
            List<EquipmentArrangement> futureArrangements = Institution.Instance().EquipmentArragmentRepository.FindAllAfter(room, this, date);

            destinationRoomArrangement.EndDate = date;
            foreach (EquipmentArrangement a in futureArrangements)
            {
                Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Remove(a);
            }

            EquipmentArrangement warehouseArrangement = Institution.Instance().EquipmentArragmentRepository.FindFirstBefore(warehouse, this, date);
            futureArrangements = Institution.Instance().EquipmentArragmentRepository.FindAllAfter(warehouse, this, date);
            DateTime newArrangementTargetEndDate = DateTime.MaxValue;


            if (warehouseArrangement is not null)
            {
                newArrangementTargetEndDate = warehouseArrangement.EndDate;
                warehouseArrangement.EndDate = date;
            }
            foreach (EquipmentArrangement a in futureArrangements)
            {
                a.Quantity += destinationRoomArrangement.Quantity;
            }

            int newWarehouseQuantity = 0;
            if (warehouseArrangement is not null)
            {
                newWarehouseQuantity = warehouseArrangement.Quantity;
            }
            newWarehouseQuantity += destinationRoomArrangement.Quantity;

            Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Add(new EquipmentArrangement(this, warehouse, newWarehouseQuantity, date, newArrangementTargetEndDate));

        }

        public void Rearrange(Room destinationRoom, Room targetRoom, DateTime newArrangementStartDate, int newArrangementQuantity)
        {
            if (targetRoom is null) throw new RearrangeTargetRoomNullException("Target room must be selected");
            else if (newArrangementQuantity == 0) throw new ZeroQuantityException("Quantity cannot be zero");
            else if (newArrangementStartDate <= DateTime.Today) throw new DateException("Arrangement date must be in future");
            else if (newArrangementQuantity > ArrangmentByRooms[destinationRoom]) throw new NotEnoughEquipmentException("Not enough equipment in selected room");
            MoveFromRoom(destinationRoom, newArrangementStartDate, newArrangementQuantity);
            MoveToNewRoom(targetRoom, newArrangementStartDate, newArrangementQuantity);
        }

        public void MoveFromRoom(Room room, DateTime newArrangementStartDate, int quantity)
        {
            EquipmentArrangement pastArrangement = Institution.Instance().EquipmentArragmentRepository.FindFirstBefore(room, this, newArrangementStartDate);
            List<EquipmentArrangement> futureArrangements = Institution.Instance().EquipmentArragmentRepository.FindAllAfter(room, this, newArrangementStartDate);

            DateTime newArrangementEndDate = pastArrangement.EndDate;
            pastArrangement.EndDate = newArrangementStartDate;
            foreach (EquipmentArrangement a in futureArrangements)
            {
                a.Quantity -= quantity;
            }

            int newDestinationRoomQuantity = pastArrangement.Quantity - quantity;
            Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Add(new EquipmentArrangement(this, room, newDestinationRoomQuantity, newArrangementStartDate, newArrangementEndDate));
        }

        public void MoveToNewRoom(Room room, DateTime newArrangementStartDate, int quantity)
        {
            EquipmentArrangement pastArrangement = Institution.Instance().EquipmentArragmentRepository.FindFirstBefore(room, this, newArrangementStartDate);
            List<EquipmentArrangement> futureArrangements = Institution.Instance().EquipmentArragmentRepository.FindAllAfter(room, this, newArrangementStartDate);
            DateTime newArrangementTargetEndDate = DateTime.MaxValue;


            if (pastArrangement is not null)
            {
                newArrangementTargetEndDate = pastArrangement.EndDate;
                pastArrangement.EndDate = newArrangementStartDate;
            }

            foreach (EquipmentArrangement a in futureArrangements)
            {
                a.Quantity += quantity;
            }


            int newTargetRoomQuantity = 0;
            if (pastArrangement is not null)
            {
                newTargetRoomQuantity = pastArrangement.Quantity;
            }
            newTargetRoomQuantity += quantity;

            Institution.Instance().EquipmentArragmentRepository.ValidArrangement.Add(new EquipmentArrangement(this, room, newTargetRoomQuantity, newArrangementStartDate, newArrangementTargetEndDate));
        }
    }
}
