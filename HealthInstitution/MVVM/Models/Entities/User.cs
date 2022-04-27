using System.Collections.Generic;
using HealthInstitution.MVVM.Models.Enumerations;

namespace HealthInstitution.MVVM.Models
{
    abstract public class User
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private Gender _gender;


        public int ID { get => _id; set { this._id = value; } }
        public string FirstName { get => _firstName; set { this._firstName = value; } }
        public string LastName { get => _lastName; set { this._lastName = value; } }
        public string Email { get => _email; set { this._email = value; } }
        public string Password { get => _password; set { this._password = value; } }
        public Gender Gender { get => _gender; set { this._gender = value; } }

        public User()
        {

        }

        public User(int id, string firstName, string lastName, string email, string password, Gender gender)
        {
            this._id = id;
            this._firstName = firstName;
            this._lastName = lastName;
            this._email = email;
            this._password = password;
            this._gender = gender;

        }

        // function that searches the collection to find the corresponding user
        public static T FindUser<T>(List<T> collection, string email, string password) where T : User
        {
            for (int i = 0; i < collection.Count; i++)
            {
                T user = collection[i];
                if (user.Email == email & user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }

    }
}
