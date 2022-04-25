using System.Collections.Generic;

namespace HealthInstitution.MVVM.Models
{
    abstract public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // ostali entiteti i getteri i setteri
        // ..........



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
