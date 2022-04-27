using Newtonsoft.Json;
using System.Collections.Generic;

namespace HealthInstitution.MVVM.Models
{
    abstract public class User
    {
        private int _id { get; set; }
        private string _firstName { get; set; }
        private string _lastName { get; set; }
        private string _email { get; set; }
        private string _password { get; set; }

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

        public User() { 
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
