namespace HealthInstitution.MVVM.Models.Entities
{
    public class Allergen
    {
        private int _id;
        private string _name;

        public int Id { get => this._id; set { this._id = value; } }
        public string Name { get => this._name; set { this._name = value; } }

        public Allergen()
        {
            
        }

        public Allergen(int id, string name)
        {
            this._id = id;
            this._name = name;
        }

    }
}