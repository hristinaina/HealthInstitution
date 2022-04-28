using HealthInstitution.MVVM.Models.Enumerations;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        // ostali entiteti i getteri i setteri
        // ..........
        [JsonProperty("ID")]
        public int ID { get => _id; set { _id = value; } }
        [JsonProperty("FirstName")]
        public string FirstName { get => _firstName; set { _firstName = value; } }
        [JsonProperty("LastName")]
        public string LastName { get => _lastName; set { _lastName = value; } }
        [JsonProperty("Email")]
        public string Email { get => _email; set { _email = value; } }
        [JsonProperty("Password")]
        public string Password { get => _password; set { _password = value; } }
        [JsonProperty("Gender")]
        public Gender Gender { get => _gender; set { _gender = value; } }

        public User() { }
        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        public User(int id, string firstName, string lastName, string email, string password, Gender gender)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;
            _gender = gender;

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
