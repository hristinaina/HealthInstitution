using HealthInstitution.MVVM.Models.Enumerations;
using HealthInstitution.MVVM.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.Models.Entities
{
    public class Patient : User
    {
        private bool _blocked;
        private BlockadeType _blockadeType;
        private MedicalRecord _record;
        public bool Blocked { get => _blocked; set { this._blocked = value; } }

        public BlockadeType BlockadeType { get => _blockadeType; set { this._blockadeType = value; } }
        public MedicalRecord Record { get => _record; set { this._record = value; } }

        public Patient()
        {

        }

        // this constructor is used when secretary is creating new patient accounts
        // - without allergen parameter
        public Patient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight)
            : base(id, firstName, lastName, email, password, gender)
        {
            this._blocked = false;
            this._blockadeType = 0;
            this._record = new MedicalRecord(height, weight);
        }


        // this constructor is used when secretary is creating new patient accounts
        // - with allergen parameter
        public Patient(int id, string firstName, string lastName, string email, string password, Gender gender,
            double height, double weight, List<Allergen> allergens)
            : base(id, firstName, lastName, email, password, gender)
        {
            this._blocked = false;
            this._blockadeType = 0;
            this._record = new MedicalRecord(height, weight, allergens);
        }


    }
}
